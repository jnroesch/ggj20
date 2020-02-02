using Game.Console;
using Game.Tasks.Abstract;
using Game.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Tasks.ConsoleTasks
{
	public class ImageTask : ConsoleTask
	{
		private List<Dictionary<string, string>> options;

		private Dictionary<string, string> option;

		public ImageTask()
		{
			options = new List<Dictionary<string, string>>();

			#region options
			options.Add(new Dictionary<string, string>()
			{
				{"word", "dog" },
				{"start", "Automatic Error Recognition System broken, please IDENTIFY ANIMAL to recalibrate" },
				{"win", "who's a good boy?" },
				{"fail", "calibration failed" },
				{"image", @"
                &                                                               
              @@@                                                               
              @@@@.                                                             
           @@@@@@@@@                                                            
         @@@@@@@@@@@@@@                                                         
       ,@@@@@@@@@@@@@@@@@@                                                      
 /@@@@@@@@@@@@@@@@@@@@@@@@@@@@@.                                                
 @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@                
   @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@            
               @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@          
                 @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@         
				  &@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@        
                    @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@        
                      @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@       
                        @@@@@@@@@@@@@@@@@@@@@@@/       @@@@@@@@@@@@@@@@@@       
						 /@@@@@@@@@@@@@@                #@@@@@@@@@@@@&@@@       
                           @@@@@@@@                       @@@@@@@@@@  @@@@      
                           @@@@@@@@                        @@@@@@@@@   @@@@@%   
                            @@@@@@@                          @@@@@@@     @@@@@@@
                            @@@@@@@                           @@@@@@@@     @@@@@
                            @@@@@@@%                          @@@@@@@@          
                            @@@,@@@@                          @@@@ @@@%         
                            @@@@@@@*                        @@@@@  @@@          
                           @@@@                                    @@@          
                          @@@                                  @@@@@@           "}
			});

			options.Add(new Dictionary<string, string>()
			{
				{"word", "fish" },
				{"start", "Automatic Error Recognition System broken, please IDENTIFY ANIMAL to recalibrate" },
				{"win", "here, have a fishy stick" },
				{"fail", "calibration failed" },
				{"image", @"
                                                                                
                                                                                
                                                                                
       .....,..                        .*#(%%%%%(                               
          ..,..#%(#(%                .# /#%#%%&%%##%%                           
          . .,..(#%%%#%%          (/(##%%&%&%%(/#(&#%&%*%%#&&&(,                
           ./((#.*%%%%#&(#%    ###%/%%%##%%/##/#/#/(%///(/*,#//./%&%            
             ***(#%%%%%%&%&(%%#%%%(/#(#(/##/#(/((* ,*(*/../*/(,#/&%%&&(%#       
               (#(((%%%#%%(*,/,/(*/*/,(#//*//,*,. /#*#%*#**,*,*%%/.,%%#&%@&&&   
                  ######(%%((/*///////,*(/(*(*((,*/*(.((*///,//#%/**#%///%%&/   
          ......,##%%%#%%%&#(/((/(**/((*/,(/*(,/(,(**(./*#((,*,((#((%#(%/(      
          ,,,/(%%%%%#%##,       #%%((*/(/((#,//,*/(//,(,(//*,,#%&((#(/.         
     ....*,*(###%#/         (#(%%%%%&##(((#((//(//(*#/#**#%&%%#(#(/             
     ,,,**(/              *                   #%%%%&&%%%%%((  %&                
                                           *#%%%%%%&&         ##                
                                          ###(((##%          #(*                
                                            .../                                
                                                                                
                                                                                "}
			});

			options.Add(new Dictionary<string, string>()
			{
				{"word", "butterfly" },
				{"start", "Automatic Error Recognition System broken, please IDENTIFY ANIMAL to recalibrate" },
				{"win", "great job, calibration succeeded" },
				{"fail", "calibration failed" },
				{"image", @"
                                                                                
                                                                                
                                                                                
                            &%                      /@                          
                            @                        /                          
     ****************(       @                      @         &************.    
   @***@  ****************     @                   @    %*********************  
    ****&********@    @******    @              @    ******************&  @***  
     #**********            @**/   &          @   @***            ***********   
       ***********   ((((((@   %**   @      *   **&   *((((((    ********@      
     ****/****           ((((((   *@      *   %*   ((((((%         *********    
     *********   @%%%%%%%%#   %((@  *  (     *  (((@   @%%%%%%%%@   ***  ***@   
       *********&        *#%%%%@  (% * (/(  @ (&  #%%%%%#         @********%    
         *******   /////////#    @%   *( . @@  %@       &#//(/   /******        
        ***% ***     @////////%@#.    @* **   .@%////////////&    *******       
         ***********%&(/%&&**&((((((@(** **(((((((@*@.         /*********       
              % ********/(((((.  @@(**** ****( (  /(((((@************@          
              *******(((((    ( ((/***&  @ ***@( ,(.  @(((((******@             
             /*****((((    ,( .((****   &   ****(( .(.   /((((*****             
             ******(((   (((((((****         ****@((((((   (((******            
              *****(((((((*@%******           ******((@((((((#*****@            
              ********************,           @********************             
               ******************               ******************              
               ***********                            ************              
      *.      **#                                               **&             
        @***&                                                      **&@***      
                                                                                
"}
			});
			#endregion

			var random = new System.Random();
			int randomInt = random.Next(options.Count);
			option = options[randomInt]; 
		}

		~ImageTask()
		{
			GameConsole.instance.OnNewSubmission -= OnConsoleInput;
		}

		public override void FailTask()
		{
			GameConsole.instance.Log(option["fail"]);
		}

		public override bool IsCompleted()
		{
			var lastInput = GameManager.Instance.GetLastConsoleInput();
			return string.Equals(lastInput, option["word"], System.StringComparison.OrdinalIgnoreCase);
		}

		public override void StartTask()
		{
			GameConsole.instance.OnNewSubmission += OnConsoleInput;
			GameConsole.instance.LogImage(option["image"]);
			GameConsole.instance.Log(option["start"]);
		}

		public override void WinTask()
		{
			SFX.Instance.PlayOneShot(SFX.Instance.GetRandomWinBeep());
			GameConsole.instance.OnNewSubmission -= OnConsoleInput;
			GameConsole.instance.Log(option["win"]);
			GameManager.Instance.FinishCurrentTask();
		}

		private void OnConsoleInput()
		{
			if (IsCompleted())
			{
				WinTask();
			}
			else
			{
				FailTask();
			}
		}
	}
}