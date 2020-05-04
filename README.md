This is a snake game made with unity 2019.3 for mobile.

However some of the code is just not the way it should be implemented which is due to a lack of prior experience with unity and c#.
One big thing is that when I want to move the snake 10 times a second by one field I just use FixedUpdate and an int variable counting to 5 and thus only taking action every 5th frame.
This should rather be implemented using coroutines but I will not fix this anymore as this is just a starting project and there are more interesting projects to be done.
