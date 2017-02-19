# EDM4606_Apparences
Tp2 Immersion

# Structure du projet

## 1_SourcesFiles
Dossier qui contiendra les différents fichiers sources des médias visuels et sonores

## 2_Documentation
Dossier qui contiendra les différents fichiers relatifs au scénario, gestion et dépenses

## 3_Project
Dossier qui contiendra les différents sous modules du projet (Touch Designer, Unity, MaxMsp)

### Unity (Jean-Christophe et Hugo)
Ce module servira principalement au rendu visuel et interactif du projet. Ce dernier recevra par Osc des ordres de la part du module Max Msp et les commandes de détection de la Kinect par le module Touch Designer

### Max Msp (Mylène et Jennifer)
Ce module servira principalement à la gestion de la structure évènementielle et sonore du projet.  Il s'occupera d'envoyer ses ordres par Max Msp au module Unity

### Touch Designer (Jean-Christophe et Hugo)
Ce module servira principalement à la détection de la kinect et à l'interpretation des données de cette dernière. Les résultats seront envoyés par Osc à Unity

### Prototype Test Room (Jean-Christophe)
Ceci est un environnement pour tester visuel, programmation et rendu sur l'écran de la salle Tokio sans affecté le projet. Ce projet a toujours un build d'actif pour le tester à tout momment.

## 4_Final
Dossier contenant le projet final avec un script d'auto exécution des différents modules.
