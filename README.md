# mixed-reality


This is a project with Hololens to investigate how the mixed reality affect quiet stance. In this project, we presented solid object and text randomly, in both a static and a dynamic setting. We measured the head movements using the built-in tracking mechanism of the Hololens,and sent data to a net server.

We used a six scripts to control actions of visual events, collect data and send data from client to server, see below.      


--Scripts
    --Lab_1_Controller.cs
    --Lab_1_StimulusBase.cs
    --Lab_1_DynamicText.cs
    --Lab_1_StaticText.cs
    --Lab_1_StaticObject.cs
    --Lab_1_DynamicObject.cs

    Lab_1_Controller.cs: controll the action of each visual event, the time clock, and net message. Add this script to 'Lab_1_Controller' as a component. Add four kinds of visual events to 'Obj list' manually. Add 'Lab_1_text_finish' to 'Last flag'

    Lab_1_StimulsBase.cs: the basic class of all visual events including all basic action. The attribution of each visual event has been attached with the individual script, see below.
    
    Lab_1_DynamicText.cs: set stimulus parameters for dynamic text. Add this script to 'Lab_1_StaticText'.

    Lab_1_StaticText.cs: set stimulus parameters for static text. Add this script to 'Lab_1_DynamicText'

    Lab_1_StaticObject.cs: set stimulus parameters for static object. Add this script to 'basketball_dynamic' 

    Lab_1_DynamicObject.cs: set stimulus parameters for dynamic object. Add this script to 'basketball_static'

net message format:

    camera data format: the tpye of visual event, time, position.x, position.y, position.z, angle.x, angle.y, angle.z;
    
    visual events data format: the tpye of visual event, action start time, action end time;


Acknowledgement:
The author Gaiqing are grateful to Shichun Hu for great help on coding and discussing, to Emil Davchev and Boris Bahariev for providing technical support on this project.
