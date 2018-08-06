# Gaming-God
C#-Bot written with the Discord.Net library.

TODO: description


# CODE admission challenge

> Find someone who has a problem - someone you can help. That “someone” can be a person, an organization, an institution or a community.

My fellow students at school

> Identify the problem and develop an approach to solve that problem - Your solution should be digital, of course.
Currently the replacement plan (Vertretungsplan) of our school is very annoying to look up:
- You don't know when the plan updates, that's why you need to refresh often and check for changes
- The website is completely unsuitable for mobile devices
- It is poorly formatted and requires you to read through a lot of content and search something that might matter to you

For these reasons I tried to automate the process by creating a bot that looks up the replacement plan every minute and compares it with the schedule of each student. when a student is affected, an email is sent to notify them about the change. 
After realising that looking up emails is somewhat bothersome too I thought to myself why not make a discord bot to notify us, because it's an app almost everyone has and frequently uses.

> After that - start realising your solution.
I started off with figuring out how to send automated messages in discord. For this I am using an open source library called [Discord.NET](https://github.com/RogueException/Discord.Net). My schoolmates and me already have a shared discord server, so I signed up a discord bot and hooked it onto that server. After that I looked into downloading and parsing PDF files because the replacement plan is only available as PDF file. This was much harder to pull off but eventually I found a library called pdfbox that enabled me to strip all text from a given PDF file so I can do the formatting via plain string formatting. Then all that was left to do was to gather all the timetables of my fellow students and put it all together.

> Why did you pick this particular organisation, institution or individual?
Because I'm part of that "community" and thus affected by the problem myself.

> How did you identify the problem to solve
I often get too lazy to look up the schedule myself and just waited until someone else did so I can ask them. eventually I figured that this "someone" doesn't necessarily need to be a human being.

> why did you pick this problem over others?
Because when I told others about my idea they were hyped about it.

> Describe your approach. How did you come up with it?
the problem was that we had to go to the source of the information, which, no matter how executed, is a hassle because of the problems listed above. eventually I wondered if instead I could make the information come to us and thats how I came up with making a bot.

> Is it the best possible solution? Why?
It probably is, or atleast I cant think of an even more efficient way to be informed about scedule changes than via automated instant notification that shows up on both pc and mobile without the need to install anything that I dont already have.
