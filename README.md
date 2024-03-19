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

### 1.3 Les issues : 
<br />

&emsp;&emsp;&emsp;&emsp;Les **Issues** github sont un bon moyen de garder en mémoire les choses importantes à **ajouter**, **changer** ou encore **implémenter** avant différentes **milestones**. Vous les retrouverez ici :  <br /> <br />

![Image montrant ou se situe les issues Github](https://media.discordapp.net/attachments/747378568572567562/1219690261563572366/image3.png?ex=660c3813&is=65f9c313&hm=52c5a659f570722e6bbfbc3d1c4c09c9414e5acca08ffcf16af79287d5539adb&=&format=webp&quality=lossless&width=1125&height=676)  <br /> <br /> 


Les **issues** se présenteront tel quels	: <br /> <br />

![Image montrant ou se situe les issues Github](https://media.discordapp.net/attachments/747378568572567562/1219690044659339395/image2.png?ex=660c37df&is=65f9c2df&hm=e793ae68cc4c08ca4d780d515bf1bf71ad7afb050d298887406d0630e3f19d5f&=&format=webp&quality=lossless&width=1440&height=305) <br /> <br /> 

Il est possible via cet onglet de chercher dans les issues par leur **nom**, mais aussi par **labels**, **milestones** ou **personne** **associés** que nous verront plus tard. 

Pour **créer** une issue, il vous suffit de **cliquer** sur le bouton “**Nouvelle** **issue**”, et de la rédiger avec cette **méthodologie** : 

![créer issue](https://media.discordapp.net/attachments/747378568572567562/1219690044949004329/image1.png?ex=660c37df&is=65f9c2df&hm=d0593feb763717afe83540375a5d498fa4d92e02e03b295872a6724e20c84a3e&=&format=webp&quality=lossless) <br /> <br />

Ici encore on retrouvera un **texte** et une **description** à ajouter, ici vous pouvez être plus **précis** dans le titre, mais les **détails** devront se trouver dans la **description**, vous pouvez même ajouter des **images** etc. 
>[!NOTE]
> A **droite** on trouvera un panel qui permet rapidement de **détailler** encore plus l’issue grâce à des labels, la milestone associé et qui est assigné a l'issue. 



### 1.3 Les Commits : 
<br />

&emsp;&emsp;&emsp;&emsp;Lors de vos **commits**, vous serez amené à ajouter un **titre** et une **description** au **commit**. Afin de mieux suivre les **updates** et les **travaux** de chacuns sur le projet, il est **important** d’avoir des commits __clairs__ et **concis**. <br /> <br />


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






### 1.4 Les bon réflexes : <br /> <br /> 

	
&emsp;&emsp;&emsp;&emsp;Grâce à eux, vous viendrez moins souvent me dire que votre scène est corrompue  : 

- Prendre l’habitude de **fetch** et **pull** votre branche à chaque fois que vous commencez à travailler.

- Faire des **commit** le plus **souvent** **possible**.

- Ne pas merge dans la **Develop** avant d’avoir entièrement fini son ajout et son issue afin de garder un bon contrôle sur les **éventuels** conflits.

- Travailler au maximum sur des **préfabs** afin de **simplifier** la production.

>[!IMPORTANT]
>Pensez à être **concis** mais **efficace** quand vous créer une branche feature, et n’oubliez pas de mettre vos **initiales** pour savoir qui travaille dessus. Ex : **AddAnimToPlayer_S**.
