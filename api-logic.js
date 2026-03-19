// On change pour une URL de recherche plus "directe"
const DVF_API_OPEN = "https://public.opendatasoft.com/api/records/1.0/search/?dataset=demandes-de-valeurs-foncieres";

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
}