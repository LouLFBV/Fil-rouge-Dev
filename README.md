# Fil-rouge-Dev

## 2- API


🚀 Guide de Développement : IA Analyse Immobilière (Gratuit & Open Data)Ce document explique comment exploiter les données publiques françaises (DVF et DPE) pour créer une application d'aide à la décision stratégique sans frais d'API élevés.

🏗️ 1. Les Piliers de la Donnée ImmobilièrePour une analyse stratégique, il faut croiser la valeur financière (le prix) et la valeur technique (l'énergie).

📊 L'API DVF (Demande de Valeurs Foncières)Gérée par la DGFiP, elle contient l'historique réel des ventes.Source : dvf-api.data.gouv.frDonnées clés :valeur_fonciere : Le prix de vente réel.surface_reelle_bati : La surface en m^2.date_mutation : Date de la vente (permet de voir l'évolution).Usage Stratégique : Calculer le prix au m^2 réel d'un quartier pour détecter les biens sous-évalués.

⚡ L'API DPE (Diagnostic de Performance Énergétique)Gérée par l'ADEME, elle recense la santé énergétique des bâtiments.Source : data.ademe.frDonnées clés :classe_consommation_energie : Note de A à G.estimation_cout_annuel_energie : Coût des factures.Usage Stratégique : Identifier les "passoires thermiques" (F ou G) pour négocier une forte décote à l'achat.

Architecture logicielle et choix technologiques :

Interface Frontend (HTML5 / CSS3) : Développement d'une Single Page Application (SPA) minimaliste privilégiant une expérience utilisateur (UX) fluide pour les agents immobiliers sur le terrain. L'interface est conçue pour être "responsive", permettant une consultation sur tablette ou poste fixe en agence.

Logique de traitement (JavaScript ES6) : Utilisation d'un moteur asynchrone pour la manipulation des données foncières. Le script assure le nettoyage des données (filtrage des dépendances et valeurs aberrantes), le calcul des moyennes pondérées par zone géographique et l'analyse prédictive de négociation.

Interopérabilité (API REST) : L'application communique avec l'API Géo de l'État pour la normalisation des données (conversion Code Postal vers Code INSEE). Ce choix garantit l'intégrité des données de recherche avant l'interrogation des bases de données de transactions.

Stratégie de résilience (Mocking Data) : Pour pallier les limites de connectivité et les restrictions de sécurité (CORS) des API publiques, une couche de données locale a été implémentée. Cette architecture permet la continuité de service en cas d'indisponibilité des serveurs gouvernementaux, assurant ainsi aux 12 agences un outil opérationnel en toute circonstance
