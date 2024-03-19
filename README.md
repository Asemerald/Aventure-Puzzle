# Méthodo / Nomenclature projet Unity


## 1. GIT  

### 1.1 Les Branches : 


&emsp;&emsp;&emsp;&emsp;Pour les branches, on travaille sur avec une Main, une branche Develop et des branches Feature : 

- La Main sera là pour répertorier les versions et milestones dans leur état final, et sera donc mise à jour peu fréquemment. 

- La branche Develop sera elle la version actuelle sur laquelle on travaille, la Main temporaire en quelque sorte. 

- Les branches Feature quant à elle seront créées par vos soins avant d’intégrer chaque feature, puis merge dans la branche Develop et fermé quand la feature a été terminé. 

![Image exemple GitFlow](https://cdn.discordapp.com/attachments/747378568572567562/1219671476496633969/Gitflow.png?ex=660c2694&is=65f9b194&hm=71341c2bfb81db36941eac2af060da8d84a38c5e2383d6ffae0b77079f9b1f4b&)


&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;_Exemple de Workflow_

Exemple :                                       	

&emsp;&emsp;&emsp;&emsp;Si vous devez intégrer les assets du puzzle 2, vous créez une branche AddAssetsLvl2_[Initiale] à partir de la branche Develop qui sera V1.1. 

Une fois l’intégration terminée, vous pouvez merge AddAssetsLvl2_T dans la branche V1.1, puis supprimer la branche AddAssetsLvl2_T.

Une fois les objectifs de la V1.1 terminé, elle est merge dans la Main, supprimé et on crée une branche V1.3 a partir de la Main fraîchement mis à jour. 

### 1.2 Nomenclature des Commits : 

&emsp;&emsp;&emsp;&emsp;Lors de vos commits, vous serez amené à ajouter un titre et une description au commit. Afin de mieux suivre les updates et les travaux de chacuns sur le projet, il est important d’avoir des commits clairs et concis. 


Exemple avec 2 cas possibles :

• Modification/Ajout de Prefabs/Models/Textures/Scripts/… : 
Nommez le commit avec le nom du dossier et y préciser simplement le contenu ajouté.

• Modification/Ajout de LD/Intégration : 
Nommez le commit avec le nom de la scène modifiée ainsi que le/les modules modifiés.


Important : Soyez concis dans le titre du commit et ajoutez des détails dans la partie description. Précisez la version de cette modification si possible.

Situation n°1					Situation n°2


Par exemple, évitez ce genre de commit : 



Les erreurs ici ? Des noms qui ne veulent rien dire et trop de fichiers changés d’un coup dû à des commits toute les semaines.






1.3 Les bon réflexes : 

	
Grâce à eux, vous viendrez moins souvent me dire que votre scène est corrompue  : 

Prendre l’habitude de fetch et pull votre branche à chaque fois que vous commencez à travailler.

Faire des commit le plus souvent possible.

Ne pas merge dans la Develop avant d’avoir entièrement fini son ajout afin de garder un bon contrôle sur les éventuels conflits.

Travailler au maximum sur des préfabs afin de simplifier la production.

Pensez à être concis mais efficace quand vous créer une branche feature, et n’oubliez pas de mettre vos initiales pour savoir qui travaille dessus. Ex : AddAnimToPlayer_S.
