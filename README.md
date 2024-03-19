# Méthodo / Nomenclature projet Unity


## 1. GIT  

### 1.1 Les Branches : 

<br />
&emsp;&emsp;&emsp;&emsp;Pour les branches, on travaille sur avec une Main, une branche Develop et des branches Feature : 

- La **Main** sera là pour répertorier les **versions** et **milestones** dans leur état final, et sera donc mise à jour peu **fréquemment**. 

- La branche **Develop** sera elle la version actuelle sur laquelle on travaille, la Main **temporaire** en quelque sorte. 

- Les branches **Feature** quant à elle seront **créées** par vos soins avant d’intégrer chaque feature, puis merge dans la branche **Develop** et fermé quand la **feature** a été **terminé**. 

![Image exemple GitFlow](https://cdn.discordapp.com/attachments/747378568572567562/1219671476496633969/Gitflow.png?ex=660c2694&is=65f9b194&hm=71341c2bfb81db36941eac2af060da8d84a38c5e2383d6ffae0b77079f9b1f4b&)


&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;_Exemple de Workflow_

<ins>Exemple :</ins>                                      	

&emsp;&emsp;&emsp;&emsp;Si vous devez intégrer les assets du puzzle 2, vous créez une branche **AddAssetsLvl2_[Initiale]** à partir de la branche **Develop** qui sera **V1.1**. 

Une fois l’intégration **terminée**, vous pouvez merge **AddAssetsLvl2_T** dans la branche **V1.1**, puis **supprimer** la branche **AddAssetsLvl2_T**.

>[!NOTE]
>Une fois les objectifs de la V1.1 **terminé**, elle est merge dans la **Main**, **supprimé** et on crée une branche **V1.3** a partir de la **Main** fraîchement mis à jour.
    
  <br />

### 1.2 Nomenclature des Commits : 
<br />
&emsp;&emsp;&emsp;&emsp;Lors de vos **commits**, vous serez amené à ajouter un **titre** et une **description** au **commit**. Afin de mieux suivre les **updates** et les **travaux** de chacuns sur le projet, il est **important** d’avoir des commits **clairs** et **concis**. <br />


<ins>Exemple avec 2 cas possibles :</ins>

• Modification/Ajout de Prefabs/Models/Textures/Scripts/… : 
Nommez le **commit** avec le **nom** du **dossier** et y **préciser** **simplement** le contenu **ajouté** sans **oublier** le **tag** de l'issue lié.

• Modification/Ajout de LD/Intégration : 
Nommez le **commit** avec le nom de la **scène** **modifiée** ainsi que le/les modules modifiés sans **oublier** le **tag** de l'issue lié.

>[!IMPORTANT]
> Soyez concis dans le **titre** du commit et ajoutez des **détails** dans la partie **description**. Précisez la **version** de cette modification si possible.

>[!CAUTION]
> SURTOUT N'OUBLIEZ PAS LE **#** DE L'ISSUE LIÉ A VOTRE **COMMIT** DANS LE **TITRE** OU LA **DESCRIPTION**.






![Image exemple des commits](https://media.discordapp.net/attachments/747378568572567562/1219682856771649668/ex-removebg-preview.png?ex=660c312d&is=65f9bc2d&hm=d621a8a07fbf9aef2c9b8814562e9b5db329043ed783655a899b81ce6da924cd&=&format=webp&quality=lossless)  
&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;_Situation n°1_&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;_Situation n°2_ <br /> <br /> 

  
<ins>Par exemple, évitez ce genre de commit :</ins>   

![Image a pas reproduire](https://media.discordapp.net/attachments/747378568572567562/1219683844916248697/image4.png?ex=660c3219&is=65f9bd19&hm=04f643013f8142aa902ee03b643b54dad5c590b77dfc775cebdafcef3913459e&=&format=webp&quality=lossless)  

>[!CAUTION]
> Les erreurs ici ? Des noms qui ne veulent **rien** **dire** et **trop** **de** **fichiers** **changés** d’un coup dû à des commits toute les semaines. <br /> <br /> 






### 1.3 Les bon réflexes : <br /> <br /> 

	
&emsp;&emsp;&emsp;&emsp;Grâce à eux, vous viendrez moins souvent me dire que votre scène est corrompue  : 

- Prendre l’habitude de **fetch** et **pull** votre branche à chaque fois que vous commencez à travailler.

- Faire des **commit** le plus **souvent** **possible**.

- Ne pas merge dans la **Develop** avant d’avoir entièrement fini son ajout et son issue afin de garder un bon contrôle sur les **éventuels** conflits.

- Travailler au maximum sur des **préfabs** afin de **simplifier** la production.

>[!IMPORTANT]
>Pensez à être **concis** mais **efficace** quand vous créer une branche feature, et n’oubliez pas de mettre vos **initiales** pour savoir qui travaille dessus. Ex : **AddAnimToPlayer_S**.
