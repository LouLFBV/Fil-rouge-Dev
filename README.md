# 🏠 Ymmo Pro - Plateforme Centrale de Gestion Immobilière & Data Science

## 📋 Présentation du Projet
Ymmo Pro est une application web d'aide à la décision et de gestion centralisée conçue pour un groupe immobilier national comprenant un siège social à Aix-en-Provence et un réseau de **12 agences partenaires** réparties sur le territoire national. 

La solution interconnecte une application robuste en **C# (ASP.NET Core Razor Pages)** gérant le catalogue opérationnel avec un module de **Data Science & Intelligence Artificielle en Python** pour analyser les tendances du marché, identifier les opportunités et estimer automatiquement la valeur des actifs (Appartements, Maisons, Terrains).

---

## 🚀 Fonctionnalités Clés (Partie DEV)
* **Gestion du Catalogue d'Actifs :** CRUD complet permettant aux agents d'ajouter, modifier et archiver des biens immobiliers selon 3 typologies précises : *Appartements*, *Maisons* et *Terrains*.
* **Tableau de Bord Décisionnel (Business Intelligence) :** Restitution d'indicateurs clés de performance (KPIs) globaux et sectoriels.
* **Module d'Analyse Prédictive (Machine Learning) :** Intégration d'un modèle mathématique capable d'anticiper les prix du marché et de guider les investissements.
* **Interface Responsive & Accessible :** Design moderne conforme aux standards du web (KISS, UX fluide, adaptabilité Mobile/Tablette/Desktop).

---

## 🛠️ Architecture Technique & Technologies

### 💻 Backend & Application Web
* **Framework :** ASP.NET Core 8.0 (Razor Pages)
* **Architecture & Bonnes Pratiques :** * Respect strict des principes **SOLID** (Responsabilité unique pour les contrôleurs, Injection de dépendances pour le contexte de données).
  * Code épuré appliquant les règles **DRY** (Don't Repeat Yourself) et **KISS** (Keep It Simple, Stupid).
* **Accès aux données (ORM) :** Entity Framework Core avec approche *Code-First*.

### 📊 Base de Données (SQL Avancé)
* **SGBD :** Microsoft SQL Server
* **Modélisation :** Schéma relationnel normalisé mettant en relation les tables clés du domaine :
  * `Agences` : Identifiant, Nom, Ville d'implantation.
  * `BienImmobiliers` : Identifiant, Surface, Prix, Type (Enum), Ville, Foreign Key vers l'Agence affiliée.
* **Requêtage Linq/SQL :** Utilisation de requêtes de regroupement avancées (`GroupBy`, `Average`, `Count`) pour alimenter les statistiques décisionnelles en temps réel.

### 🐍 Module Data Science & IA (Python)
* **Bibliothèques :** `Pandas`, `NumPy`, `Matplotlib`, `Seaborn`, `Scikit-Learn`.
* **Traitement & Nettoyage (Data Cleaning) :** Script autonome gérant le nettoyage des données brutes, le formatage des variables numériques et le calcul de métriques avancées (Prix au m²).
* **Algorithme d'IA :** Implémentation d'un modèle de **Régression Linéaire (Machine Learning)** permettant de cartographier la tendance générale des prix et d'estimer instantanément la valeur financière d'un bien en fonction de sa surface.

---

## 📂 Structure du Code Source
```text
📦 YmmoPro
 ┣ 📂 Data
 ┃ ┗ 📜 ApplicationDbContext.cs   # Contexte EF Core, déclaration des DbSet (SQL Avancé)
 ┣ 📂 Models
 ┃ ┣ 📜 Agence.cs                 # Modélisation POO de l'entité Agence
 ┃ ┗ 📜 BienImmobilier.cs         # Modélisation POO de l'entité BienImmobilier
 ┣ 📂 Pages
 ┃ ┣ 📂 BienImmobiliers           # Pages du CRUD opérationnel (Create, Read, Update, Delete)
 ┃ ┣ 📜 Tendances.cshtml          # Vue HTML responsive présentant la data et les rapports
 ┃ ┗ 📜 Tendances.cshtml.cs       # Contrôleur C# (Calculs statistiques LINQ / SQL)
 ┣ 📂 wwwroot
 ┃ ┣ 📜 predictions_ia.png        # Graphique prédictif exporté par Python
 ┃ ┗ 📜 tendance_villes.png       # Graphique de répartition sectorielle exporté par Python
 ┣ 📜 analyse.py                  # Script Python d'Analyse de Données et Machine Learning
 ┗ 📜 Program.cs                  # Point d'entrée de l'application & Injection de dépendances
```
---

## ⚙️ Installation et Exécution

### 1. Prérequis
* Visual Studio 2022 (avec la charge de travail *Développement Web*)
* .NET 8.0 SDK
* Python 3.10+ (Ajouté au `PATH` système Windows)

### 2. Configuration des dépendances Python
Ouvrez un terminal (ou le PowerShell développeur de Visual Studio) à la racine du projet et installez l'environnement de Data Science requis :
```bash
pip install pandas numpy matplotlib seaborn scikit-learn
```
3. Génération des rapports Data Science (Python)
Pour exécuter le script d'analyse de données et générer les graphiques décisionnels directement au sein des assets de l'application web :

```bash
python analyse.py
```
Le script va simuler le traitement du volume du catalogue sur les 12 villes cibles et exporter les rendus visuels mis à jour directement dans le dossier wwwroot/.

### 4. Lancement de la Plateforme Web (C#)
Ouvrez la solution sur Visual Studio.

Appliquez les migrations de base de données via la Console du Gestionnaire de packages : Update-Database.

Pressez Ctrl + F5 pour exécuter l'application sans débogage.

Naviguez vers l'onglet Tendances du Marché pour visualiser l'ensemble des indicateurs SQL et l'intégration des graphiques prédictifs de l'IA Python.

### 🎯 Validation des Critères de Soutenance
- Développement C# & POO : Modélisation d'entités claires, respect des patterns d'architecture backend et isolation du code.
- Principes SOLID / DRY : Séparation nette entre l'administration opérationnelle du catalogue (CRUD) et l'analyse décisionnelle (Page Tendances et script Python).
- SQL Avancé : Agrégations et requêtes optimisées pour extraire le volume d'annonces et les prix moyens par secteur géographique sur les 12 villes.
- Traitement de données Python : Pipeline complet allant du nettoyage de la donnée (Data Cleaning) jusqu'à l'entraînement et l'évaluation d'un algorithme de Machine Learning (Scikit-Learn).
- UI/UX & Accessibilité : Utilisation des composants utilitaires de Bootstrap assurant une fluidité visuelle complète sur l'ensemble des supports (Desktop/Mobile).
