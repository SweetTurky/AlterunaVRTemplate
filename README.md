# READ ME

# Plan for programmering af VR projekt for Roskildefestival:

Liste over scripts:
GameManager: Styrer de største dele af applikationen, som progression, "point" og afgørednde behaviour.
Scenemanager: Styrer sceneskift og bliver kaldt af gamemanageren/eventmanageren, når bestemte handlinger er foretaget.
AudioManager: Kontroltårnet for al lyd, både Carl Knasts sang, men også musik i camp (hvis vi vil have det med), samt lydeffekter (skridt, foley, ambience mm.) og evt. voice-over til introen.
VR-Rig/movement/controls/hand-gestures: Styrer alt movement og control-relateret, og indeholder også vores charactercontroller. Anbefaler at vi benytter os af det nye inputsystem, hvis vi ender med at bruge knapper til noget. Jeg ved ikke hvordan inputs og hand gestures virker sammen, men det skal vi nok finde ud af.
NavMesh: Til alle NPC-bevægelser, der ikke bare er animationer som at bobbe med hovedet eller lignende.
EventManager: Styrer forskellige events i scenen og taler sammen med AudioManageren, GameManageren og SceneManageren.
MechanicsManager: Dette script skal stå for at manifestere mechanics, som ved instantiate-funktioner (til eksempelvis visuelle lydbølger, man skal undgå), og sørge for at "gamify" vores oplevelse bedst muligt.

Generelt skal vi passe på med at scripte for meget, og gøre controls så open-world som muligt, men samtidig sørge for at events bliver kaldt korrekt, så vi ikke står i en situation, hvor folk har lyst til at udforske, men at de bliver sendt videre før tid (som ved fx. Timer-baserede events). Mit forslag er, at undgå for mange "obskure"-scripts og holde navngivningen i linje med ovenstående. Med denne oversigt, vil man kunne referere til de scripts vi bruger nemt og smertefrit. Den vil løbende opdateres som et README-dokument. #
 
