# Table of Contents

[TOC]



# About Intergalactic Phonebook

Intergalactic Phonebook (or IPB) is an application for storing your phone contacts securely and with the option to synch them to a remote server. Your contacts can optionally be protected with a password.

# Regarding this README and BitBucket

BitBucket doesn't (or can't?) display images from another relative folder in the README on their website when viewing it in the Git repository. Therefor please open this Readme using other software instead (like [Typora](https://typora.io/)).

# UML diagram (simple)

![overview](Documents\DocResources\Overview.jpg)

# Installation Instructions

1. Download the files from: https://bitbucket.org/Napoleonite/intergalacticphonebook/downloads/
2. If deploying on an Android device, please install Unity Remote 5.x from the Google Play Store on your Android device.
3. Use the latest version of Unity 2020.1.x to open it.
4. To connect the front-end to the back-end please see the installation instructions of the back-end. The back-end Git can be found [here](https://bitbucket.org/Napoleonite/intergalacticphonebook_backend).
5. Start Unity remote and connect your Android device using an USB-cable to deploy the Unity application to your Android device by using Unity (I assume everyone knows how to do this?).
6. If you receive any errors near the bottom of the login-screen then please make sure you followed all the steps from the back-end README or alternatively disable the REST in the Unity project: Open the **InitializationScene** > **RestClient** (object in scene) > Uncheck **IsEnabled**.
7. For reading the markdown documents offline I recommend [Typora](https://typora.io/).

# Troubleshooting

## Cannot connect to destination host.

### Solution

Connect the Android device to the same network as the back-end machine, make sure that the back-end is running the IIS service as well as the back-end project itself.

## Unity not recognizing your Android device

### Posible solution

Ensure that you used the proper cable, not all types of cables work because some cable are for charging only.

# @ [removed name of company]

- Some comments and decision making are explained in the Git and code comments.

- Some features and TODO-items have been left unfinished and open due to time constraints.

- Because I split the project into 2 (front-end and back-end) the back-end will have a separate README file in the main directory of the GIT.

- I had to make many choices between quality and quantity. I had no time to properly design a GUI, create proper coding and naming conventions, find the best way to implement the back-end, etc., minimal refactoring, documentation and the quality of the Git commits suffered. I ended up aiming at atleast the requirements + a very simple back-end before adding more but I think I ended up with a tad more on the front-end part.

  - The amount of time that I spent on the back-end is rather minimal. I figured that I can't do both properly enough but I want at least the minimal back-end, so I did. I'm not proud of the back-end code but I'm proud of doing it within such a limited time-frame out of nothing. I do seem to like .Net Core though.
  
- No 'real' customer, feedback and almost no communication, no proof of concept, etc. That sure does make a lot of decision-making a lot harder and the case document is on some areas rather open for interpretation and sometimes defines no limits, making the decision making harder again.

- All resources used in this project are commercially useable for me (either public domain, I bought the rights/license, self-made, etc.).

- The back-end has almost no build-in security and the Web API is unprotected.

- Like in Rimworld, the localization doesn't capitalize the first letter in the localization data. It's being capitalized using code instead to reduce the amount of translation entries. The same goes for prefixes and suffixes.

- Example of a dynamic coding (getting fields dynamically):
  
  - ```csharp
    contact.GetAllFields().ForEach(f => cmd.Parameters.Add(new SqliteParameter(string.Format("@p{0}", f.Name), f.GetValue(contact))));
    ```
  
- Imported textures with automatically-generated mip maps will look good when rendered at any size <= the original image size. However, these textures must have a power-of-two size. The 'customer' didn't provide an artist nor any placeholders or premade textures nor any other resources or means. Thus this has been ignored and one must pretend the graphics are perfect, including the importing process and such.

- I didn't encrypt the database because it would make debugging a lot harder but I have the encryption code in the project ready for use. I could have written a second encryption inheriting from IEncryption and simply not encrypt it inside (just make it pass the unit tests) but I don't see the point of doing that for this 'exercise'.

- The Git commits should each be working commits (for PC), some commits won't work on the Android device.

# Design Choices

- I chose no ECS due to the time required. The last time I used ECS my development time went up drastically and the amount of resources on the subject are rather limited compared to non-ECS.
- I usually prefix my prefabs in Unity because I had some problems in the past where I confused the actual prefabs with prefab instances.
- GUI: Obviously my knowledge here is not what I need it to be. I need to spend a few days figuring out, reading up and making some test projects to figure out how to best create Unity GUI's for **<u>all</u>** possible Unity resolutions while also ensuring a good architecture. I don't have a few days for just the GUI therefore I had to skip on some parts, knowing it isn't ideal.
- The GUI elements of the contacts menu **had to be** be pooled. There's a huge performance hit on the Android Go6 at just 250 contacts. I strongly suspect that the Unity GUI performance optimization is extremely terrible compared to even .Net WinForms.

# Some of the used skills, techniques & tooling

- Auto rotation is supported (for mobile Android devices)
- .Net Core (a bit)
- Code-First
- Design Patterns and related
  - Object Pooling (for UI elements)
  - Repository
  - Singleton
  - UI stack data implementation
- [Draw.io](https://www.draw.io/)
- [Gimp](https://www.gimp.org/)
- Git
- Localization (hand-written, not the build-in one)
- Microsoft SQL Server
- Multi-user
- REST API
- Security by design (MD5, Login, IPB will attempt to crash you if you try to access someone else's data, encryption-ready, etc. )
- Sqlite
- Unity 2020.x / C#
- Visual Studio 2019 (CE)
- XML

# Open todo-items

1. Because I use 1 scene for all my menus, I could relatively easy make them transition animated (slide them in/out to/from a random direction).
2. User registration.
3. Contact deletion (not required by customer?).
4. Refactor: Asset locations and naming are not ideal and some are too long. I had no time to plan this ahead.
5. Automated database backups.
6. Back-end (in general).
7. Synching the two databases properly
   1. Also: back-end database requires a LastSeen column in each table for synching.
8. More unit tests.
9. Improved security.
10. The GUI requires a graphical artist.
11. Settings button on the Login menu that can among others:
    1. Edit remote url.
    2. Toggle remote connection.
    3. Set font.
    4. Toggle encryption.
    5. Change/Toggle background.
12. Various options in the login menu regarding working offline, synching, etc.
13. Finish the AudioMgr.
14. Possible option (I'm not sure yet): If the database is empty (also after synch), ask the user **once**: "We noticed that you have no contacts, would you like to load some sample contacts"?
15. Bug-fixing.
16. A lot of refactoring.
17. A lot of testing.

# Known bugs
- If there are 3 (max) popups while another one is added then the position of all popups stutters shortly.
- Cats sometimes get hit even when the player is not aiming at them.

# Application Manual

## Loading screen

<img src="Documents\DocResources\Loading.jpg" align="left" alt="Loading" style="zoom:25%;" />

## Login
<img src="Documents\DocResources\Login.jpg" align="left" alt="Login" style="zoom:75%;" />

- Click on the language icons in the top right to change language.
- If you haven't got an account yet then enter your e-mail and password and click [Register]. A popup will inform you about the results.
- After having set the correct login credentials, click [Login].
- If an error occurred while connecting to the remote server then it'll be displayed at the bottom.
- Use the checkboxes to remember your login data. Please don't remember the password on a device that's shared with other people.

### Developer notes

<img src="Documents\DocResources\LoginDevNotes.jpg" align="center" alt="LoginDevNotes" style="zoom:100%;" />

Please note that I left the debugging options in the InitializationScene turned on. This means that on every app-start the database will be emptied and filled with 50 random contacts and two default users. The default user for testing: "**a@a.com**" with password "**1a**".

## Contacts menu

<img src="Documents\DocResources\ContactsMenu.jpg" align="left" alt="ContactsMenu" style="zoom:50%;" />

1. Click on this button to open the sorting menu to sort your contacts.
2. This is the settings buttons. You can also logout through the menu that this button opens. It also contains 'something extra'.
3. All of your contacts.
4. Search/Filter your contacts.
5. Add a new contact (the [+] button).
6. Edit the selected contact. If the button is red then you haven't selected a contact in #3.
7. Displays basic contact information about the currently selected contact.

## Add/Edit a contact

<img src="Documents\DocResources\AddEditContactMenu.jpg" align="left" alt="AddEditContactMenu" style="zoom:75%;" />

- Here you can add/edit a contact. Fill in the fields (none are required) and click on [Save] (or [Add] if you were adding one) to save & apply your changes.
- Clicking [Revert] will revert all fields to their default values.

## Gyro minigame

From the contacts menu click on the settings button and then click on the cat symbol. Aim your phone to shoot cats. Note that this only works if your phone supports and has a gyroscope, otherwise the menu will tell you and won't allow you to enter the minigame.

# Conventions used

## Unity Naming Conventions

| Prefix | Type    |
| ------ | ------- |
| P_     | Prefab  |
| T_     | Texture |

| Suffix | Type           |
| ------ | -------------- |
| _V     | Prefab Variant |

## Database

- All table names must be singular (except ones not created by us perhaps). DON'T start them with underscores.

- Field names use UpperCamelCase (=PascalCase).

# Git

1. Separate subject from body with a blank line.
2. Capitalize the subject line, there will only be one subject line.
3. Do not end a subject line with a period.
4. Body lines always start with a capital letter.
5. End body lines with a period (unless the last part contains code because this could make it confusing).
6. Use the imperative mood (ex. "Add a new button", NOT "Added a new button" or "Adds a button") in the subject line.
7. Use the body to explain what and why vs. how.
8. Bullet points are okay.

# Unity (C#)

1. private accessors are always written.
2. Use String (capitalized) when referring specifically to the class; otherwise use string.
3. Attempt to prevent hardcoded values as much as possible. Use a Constants class if needed.
4. Always close a database-connection before disposing it. See also https://stackoverflow.com/questions/61092/close-and-dispose-which-to-call

# Unity texture suffix naming convention

![unity_texture_naming_conv](Documents\DocResources\UnityTextureNamingConv.png)


# Abbreviations

| Abbreviation | Meaning                  |
| ------------ | ------------------------ |
| Btn          | Button                   |
| Docs         | Documents                |
| HC           | HardCoded                |
| IPB          | Intergalactic Phone Book |
| Mgr          | Manager                  |
| UX           | User Experience          |
