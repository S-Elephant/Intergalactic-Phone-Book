# Quick Temporary Notes & Brainstorming

- Unity and DLL's... I wonder if it's any good? Virtually nobody seems to use it.
  - Personally I'd probably just share the files "as a link" in Visual Studio but for this project that seems pointless.
- Unity still has the blurry text problem: https://answers.unity.com/questions/1226551/ui-text-is-blurred-unity-535f.html?_ga=2.122510472.2076689340.1596642134-510999355.1596189980
- The project itself seems rather simple, it's mostly a matter of time-management.
- I should instantiate the contact buttons in the login screen already. Instantiating costs quite a bit of performance, even in ECS. So I guess I could do some pooling here.
  - YES, I should/must! On an old Android phone even just 250 records causes a performance hit without pooling.
- It seems that adding Sqlite to Unity in Android is rather 'quirky'. I got it working but not a single tutorial that I could find that is up2date enough to get it working. On top of that one <u>must</u> rename it to Db.**bytes** in order for it to work. I remember this to be the case but why does my Xml-file work without the .bytes extension then? I miss some knowledge here. Of course, one must also manually (through code) copy the database file. Either I'm doing it way too complicated or it just is?
- https://stackoverflow.com/questions/61092/close-and-dispose-which-to-call So yes do call .Close() even though it's not required.
- Brainstorm: Howto quickly pool the scrollview?
  - Perhaps the cheesiest way would probably be to leave the first and last button on there always (this ensures that the scrollview can actually scroll that deep), remove the vertical layoutgroup and manually position the items based on the scrollposition.
  - The better and funnier way would be to create an infinite scroll-view (which I wanted anyway). But it would leave me with time issues unless I happen to find a really good tutorial/example.
    - Quickest solution?
      - As I suspected, when I look at how other devs did it, their code is massive to implement this well. The best way would be to.... Just download one.
  - **Final solution:** I'm going to download one and alter that one at some point the future (if I have time).
- I just realized that I completely forgot that we should also be able to search for contacts (makes sense). I have no more room in my GUI for this. I guess I'd have to redo it a bit.

