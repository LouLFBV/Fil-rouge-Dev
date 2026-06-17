import pandas as pd
import numpy as np
import matplotlib.pyplot as plt
import seaborn as sns
from sklearn.model_selection import train_test_split
from sklearn.linear_model import LinearRegression

print("--- 1. GÉNÉRATION AUTOMATIQUE DES DONNÉES (12 VILLES & 3 TYPES) ---")
np.random.seed(42)
n_biens = 250  # Augmentation du nombre de biens pour couvrir les 12 villes

# Liste des 12 villes demandées
villes_liste = [
    "Paris", "Aix-en-Provence", "Lyon", "Marseille", "Bordeaux", "Lille", 
    "Nantes", "Strasbourg", "Toulouse", "Nice", "Montpellier", "Rennes"
]

# Les 3 types de biens demandés
types_liste = ["Appartement", "Maison", "Terrain"]

villes = np.random.choice(villes_liste, n_biens)
types = np.random.choice(types_liste, n_biens)

# Génération de surfaces cohérentes selon le type de bien
surfaces = []
for t in types:
    if t == "Appartement":
        surfaces.append(np.random.randint(25, 120))   # 25 à 120 m²
    elif t == "Maison":
        surfaces.append(np.random.randint(80, 250))   # 80 à 250 m²
    else: # Terrain
        surfaces.append(np.random.randint(200, 1500)) # 200 à 1500 m²
surfaces = np.array(surfaces)

# Calcul d'un prix cohérent (Le prix au m² d'un terrain est plus bas qu'une maison)
prix = []
for s, t in zip(surfaces, types):
    if t == "Appartement":
        p = s * np.random.randint(3500, 7000)
    elif t == "Maison":
        p = s * np.random.randint(2800, 5500)
    else: # Terrain
        p = s * np.random.randint(150, 600) # Prix au m² du foncier nu
    # Ajout d'une part de hasard
    p += np.random.randint(-15000, 30000)
    prix.append(max(20000, p)) # Éviter les prix négatifs
prix = np.array(prix)

# Création du DataFrame
df = pd.DataFrame({
    'Surface': surfaces,
    'Ville': villes,
    'Type': types,
    'Prix': prix
})

print("--- 2. NETTOYAGE ET CALCUL DU PRIX AU M² ---")
df['Prix_m2'] = df['Prix'] / df['Surface']

print("--- 3. CRÉATION DU GRAPHIQUE 1 : ANALYSE DES SECTEURS ---")
plt.figure(figsize=(12, 6)) # Élargissement de la figure pour afficher les 12 villes proprement
# countplot avec les 12 villes et les 3 types de biens
sns.countplot(data=df, x='Ville', hue='Type', order=villes_liste, palette='viridis')
plt.title("Répartition du catalogue Ymmo Pro (12 Villes - Appartements, Maisons, Terrains)", fontsize=14, fontweight='bold')
plt.xlabel("Villes partenaires")
plt.ylabel("Nombre de biens disponibles")
plt.xticks(rotation=45) # Rotation des noms des 12 villes pour éviter qu'ils se chevauchent
plt.legend(title="Type de bien")
plt.tight_layout()

# Sauvegarde directe dans wwwroot
plt.savefig('wwwroot/tendance_villes.png', dpi=150)
plt.close()
print("-> Graphique 'wwwroot/tendance_villes.png' mis à jour avec succès !")

print("--- 4. CRÉATION DU GRAPHIQUE 2 : MACHINE LEARNING (PRÉDICTIONS) ---")
# Pour le Machine Learning, on entraîne l'IA sur l'ensemble pour tracer la tendance globale
X = df[['Surface']]
y = df['Prix']
X_train, X_test, y_train, y_test = train_test_split(X, y, test_size=0.2, random_state=42)

modele = LinearRegression()
modele.fit(X_train, y_train)
predictions = modele.predict(X_test)

plt.figure(figsize=(10, 5))
# Affichage des points de couleur différente selon le type pour enrichir le graphique
sns.scatterplot(data=df, x='Surface', y='Prix', hue='Type', palette='viridis', alpha=0.7)
# Ajout de la ligne de prédiction (Régression Linéaire)
plt.plot(X_test, predictions, color='red', linewidth=2.5, label='Modèle Prédictif Global (IA)')
plt.title("Intelligence Artificielle : Estimation automatique de la valeur du bien", fontsize=14, fontweight='bold')
plt.xlabel("Surface (m²)")
plt.ylabel("Prix de vente (€)")
plt.gca().yaxis.set_major_formatter(plt.FuncFormatter(lambda x, p: format(int(x), ','))) # Formatage lisible des prix
plt.legend()
plt.tight_layout()

# Sauvegarde directe dans wwwroot
plt.savefig('wwwroot/predictions_ia.png', dpi=150)
plt.close()
print("-> Graphique 'wwwroot/predictions_ia.png' mis à jour avec succès !")
print("\n--- ANALYSE COMPLÈTE DES 12 VILLES TERMINÉE ---")