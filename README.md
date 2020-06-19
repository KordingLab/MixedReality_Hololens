# Evaluation of head movements of mixed reality on quiet stance


Mixed reality Mixed reality (MR) has promise for learning, design, and entertainment, and for use during everyday life. In this project, we instructed subjects to stand quietly, and measured how much virtual stimuli presented in mixed reality (Microsoft HoloLens) affected their quiet stance. 

We used a set of scripts to control actions of visual events, get data of head movements from Hololens, and send data from client to server.  

--Scripts

    --Lab_1_Controller.cs
    --Lab_1_StimulusBase.cs
    --Lab_1_DynamicText.cs
    --Lab_1_StaticText.cs
    --Lab_1_StaticObject.cs
    --Lab_1_DynamicObjectH.cs
    --Lab_1_DynamicObjectV.cs
    --WordConf.cs
    --WordGenerator.cs
   
    
    Lab_1_Controller.cs: control the visual events including the actions, send and save data. Add this script to 'Lab_1_Controller' as a component. Add four types of visual events to 'Obj list' manually. Attach scripts to each visual event, respectively, please see the four scripts below. Add 'Lab_1_text_finish' to 'Last flag'

    Lab_1_StaticObject.cs: set stimulus parameters for static object and text. Add this script to 'basketball_dynamic'.
    
    Lab_1_DynamicObject.cs: set stimulus parameters for dynamic object moving from back to front. Add this script to 'basketball_dynamic'.

    Lab_1_DynamicObjectH.cs: set stimulus parameters for dynamic object and text from left to right. Add this script to 'basketball_dynamicH'.
    
    Lab_1_DynamicObjectV.cs: set stimulus parameters for dynamic object with text from up to down. Add this script to 'basketball_dynamicV'.
    
    WordConf.cs and WordGenerator.cs: to generate the texts used with the object.

net message format:

    camera data format: the tpye of visual event, time, position.x, position.y, position.z, angle.x, angle.y, angle.z;
    
    visual events data format: the tpye of visual event, action start time, action end time;


Acknowledgement:
The author Gaiqing would like to thank Shichun Hu and Yuliang Ye for their assistance with the development of set-up, also thanks to Emil Davchev and Boris Bahariev for providing technical support on this project.
