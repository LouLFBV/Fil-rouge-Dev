// On change pour une URL de recherche plus "directe"
const DVF_API_OPEN = "https://public.opendatasoft.com/api/records/1.0/search/?dataset=demandes-de-valeurs-foncieres";

let moyenneLocaleGlobale = 0; // Variable pour stocker la moyenne

document.getElementById('btnAnalyse').addEventListener('click', async () => {
    const loader = document.getElementById('loader');
    const results = document.getElementById('results'); // On récupère le div des résultats
    
    loader.style.display = 'block';
    results.style.display = 'none'; // On le cache au début de la recherche

    try {
        await new Promise(resolve => setTimeout(resolve, 800));

        console.log("Utilisation des données de test locales");
        displayResults(MOCK_DATA);
        document.getElementById('cityName').innerText = "Mode Test (Données Locales)";

        // --- LA LIGNE À AJOUTER ICI ---
        results.style.display = 'block'; 
        // ------------------------------

    } catch (err) {
        alert("Erreur : " + err.message);
    } finally {
        loader.style.display = 'none';
    }
});

function displayResults(records) {
    const tableBody = document.getElementById('tableBody');
    let totalM2 = 0;
    let count = 0;
    tableBody.innerHTML = "";

    // Calcul de la tendance (compare la première et la dernière vente du tableau)
    const premiereVente = records[0].valeur_fonciere / records[0].surface_reelle_bati;
    const derniereVente = records[records.length - 1].valeur_fonciere / records[records.length - 1].surface_reelle_bati;
    const evolution = ((premiereVente - derniereVente) / derniereVente) * 100;

    const tendanceHTML = `
        <div style="background: #eee; padding: 10px; margin: 10px 0; border-radius: 5px;">
            Tendance locale : <strong>${evolution > 0 ? '📈 Hausse' : '📉 Baisse'} ${Math.abs(evolution).toFixed(1)}%</strong> sur la période.
        </div>
    `;
    document.getElementById('cityName').insertAdjacentHTML('afterend', tendanceHTML);

    // Calcul de la moyenne globale en amont
    records.forEach(r => {
        if (r.valeur_fonciere && r.surface_reelle_bati) {
            totalM2 += (r.valeur_fonciere / r.surface_reelle_bati);
            count++;
        }
    });
    const moyenneMarche = totalM2 / count;

    // Affichage avec diagnostic
    records.forEach(r => {
        const pM2 = r.valeur_fonciere / r.surface_reelle_bati;
        
        // LOGIQUE IA SIMPLE : Comparaison à la moyenne
        let diagnostic = "";
        let couleur = "black";
        
        if (pM2 < moyenneMarche * 0.9) {
            diagnostic = "🟢 Excellente Affaire (-10%)";
            couleur = "green";
        } else if (pM2 > moyenneMarche * 1.1) {
            diagnostic = "🔴 Surévalué (+10%)";
            couleur = "red";
        } else {
            diagnostic = "🟡 Prix de Marché";
        }

        tableBody.innerHTML += `
            <tr style="color: ${couleur}">
                <td>${new Date(r.date_mutation).toLocaleDateString()}</td>
                <td>${r.type_local}</td>
                <td>${r.surface_reelle_bati} m²</td>
                <td>${Math.round(pM2).toLocaleString()} €/m²</td>
                <td><strong>${diagnostic}</strong></td>
            </tr>`;
    });

    document.getElementById('avgPrice').innerText = Math.round(moyenneMarche).toLocaleString() + " €/m²";
    moyenneLocaleGlobale = moyenneMarche; 
    document.getElementById('simulateur').style.display = 'block';
}

function calculerStrategie() {
    const prix = document.getElementById('prixBien').value;
    const surface = document.getElementById('surfaceBien').value;
    const verdict = document.getElementById('verdict');

    if(!prix || !surface) return alert("Entrez le prix et la surface");

    const pM2Bien = prix / surface;
    const difference = ((pM2Bien - moyenneLocaleGlobale) / moyenneLocaleGlobale) * 100;

    if (pM2Bien > moyenneLocaleGlobale) {
        verdict.innerHTML = `⚠️ Ce bien est <span style="color:red">${difference.toFixed(1)}% plus cher</span> que la moyenne locale. <br> 
        💡 Conseil Stratégique : Négociez au moins ${Math.round(prix - (moyenneLocaleGlobale * surface)).toLocaleString()} € pour revenir au prix du marché.`;
    } else {
        verdict.innerHTML = `✅ Excellente opportunité ! Ce bien est <span style="color:green">${Math.abs(difference).toFixed(1)}% en dessous</span> du prix du marché.`;
    }
}