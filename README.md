# Fil-rouge-Dev

## 2- API


🚀 Guide de Développement : IA Analyse Immobilière (Gratuit & Open Data)Ce document explique comment exploiter les données publiques françaises (DVF et DPE) pour créer une application d'aide à la décision stratégique sans frais d'API élevés.

🏗️ 1. Les Piliers de la Donnée ImmobilièrePour une analyse stratégique, il faut croiser la valeur financière (le prix) et la valeur technique (l'énergie).

📊 L'API DVF (Demande de Valeurs Foncières)Gérée par la DGFiP, elle contient l'historique réel des ventes.Source : dvf-api.data.gouv.frDonnées clés :valeur_fonciere : Le prix de vente réel.surface_reelle_bati : La surface en m^2.date_mutation : Date de la vente (permet de voir l'évolution).Usage Stratégique : Calculer le prix au m^2 réel d'un quartier pour détecter les biens sous-évalués.

⚡ L'API DPE (Diagnostic de Performance Énergétique)Gérée par l'ADEME, elle recense la santé énergétique des bâtiments.Source : data.ademe.frDonnées clés :classe_consommation_energie : Note de A à G.estimation_cout_annuel_energie : Coût des factures.Usage Stratégique : Identifier les "passoires thermiques" (F ou G) pour négocier une forte décote à l'achat.

🛠️ 2. Étapes d'Intégration (Pas à Pas)

Étape A : Géocodage (L'Adresse vers les Coordonnées)Avant d'interroger DVF ou DPE, vous devez transformer une adresse (ex: "10 rue de la Paix") en coordonnées GPS.Outil : API Adresse (BAN) — Gratuit.Action : Récupérer la latitude, la longitude et le code_insee.

Étape B : Extraction des Ventes (DVF)Interrogez l'API DVF avec le code commune ou la section cadastrale.Exemple de requête :GET https://dvf-api.data.gouv.fr/api/v1/mutations/?code_commune=33063 (Bordeaux)

Étape C : Calcul de l'IA de TendanceUtilisez une logique simple pour générer un conseil stratégique :Moyenne du quartier : Calculez le prix moyen au m^2 sur les 2 dernières années.Comparaison : Si le bien visé est 15% moins cher que la moyenne ET que son DPE est mauvais (G).Verdict : Afficher "Opportunité de rénovation à forte rentabilité".

💻 3. Exemple de Code (Python / Backend)Voici comment automatiser la récupération du prix moyen d'une ville :


``` 
import requests

def get_market_trend(code_insee):
    url = f"https://dvf-api.data.gouv.fr/api/v1/mutations/?code_commune={code_insee}"
    response = requests.get(url)
    
    if response.status_code == 200:
        ventes = response.json()['results']
        prices_per_m2 = []
        
        for v in ventes:
            if v['valeur_fonciere'] and v['surface_reelle_bati']:
                price = v['valeur_fonciere'] / v['surface_reelle_bati']
                prices_per_m2.append(price)
        
        moyenne = sum(prices_per_m2) / len(prices_per_m2)
        return round(moyenne, 2)
    return None
```

# Utilisation pour Bordeaux
print(f"Prix moyen m2 à Bordeaux : {get_market_trend('33063')} €")