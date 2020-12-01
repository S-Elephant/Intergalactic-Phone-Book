# Phone Book Application requirements

Much like a “Contacts” application on your phone, this application should let you perform the following
tasks:

- [x] Create, search, edit a contact.
  - [x] Create
  - [x] Search
  - [x] Edit
- [x] Edit the following contact properties:
  - [x] Name
  - [x] Last name
  - [x] Description
- [x] **Add**, edit, and **remove** the following contact properties:
  - [x] Phone number
  - [x] E-mail
  - [x] Twitter handle
- [x] View an alphabetical listing of contacts
- [x] Sort the contact listing by date added

## Requirements
- [x] Code must be written in C#.
- [x] Contact data must be persistent. Save to and load from a file format of your choice.
- [x] UI quality and polish are important, including remaining clear and usable in all aspect rations
and resolutions.
- [x] The UI must be fully dynamic and instantiated on the fly from loaded data. Use prefabs and
spawning, rather than Scene tricks.
- [x] Create steps for running the application after delivery.

## Extra Points
- [x] A pooling solution for the various UI elements
- [x] You are free to add a (simple) back-end in .NET Core – you can watch tutorials to create a
basic architecture – this of course takes more time
- [x] Fully animated screen transitions (I got one transition for my 2 screens)
- [ ] ~~Fully animated UI element transforms~~
- [x] Interactive UI elements that respond to various inputs (time of day, gyroscope)
- [x] A non-flat persistent storage solution (Firebase, local SQL, surprise us)
- [ ] ~~ECS-based architecture~~
- [x] Unit tests (a few)
- [ ] ~~Your own, sprite-based UGUI replacement.~~
- [x] Privacy by design
- [x] Security by design
- [x] Other technical delights to impress us…

## Delivery
- [x] Deliver the source in your Bitbucket repository
- [x] And give access to us.
- [x] Add an appropriate git ignore file and deliver a README written in Markdown with a description of your project, and any relevant applications. The README is really important. You have to tell us how we have to run your project!
- [x] Also, explain some of your decision if you think this is of value for us. An incomplete or even no README is a reason to reject the test. We will evaluate an incomplete test if the code, comments, and documentation (README) are good enough.