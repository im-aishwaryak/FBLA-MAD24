import 'package:flutter/material.dart';
import 'home.dart'; 
import 'level.dart'; 


class ProgressScreen extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: PreferredSize(
        preferredSize: Size.fromHeight(40), 
        child: AppBar(
          leading: IconButton(
            icon: Icon(Icons.home, color: Colors.black),
            onPressed:(){
              Navigator.push(
                            context, 
                            MaterialPageRoute(builder: (context) => HomePage()),
                            );
            }
        ),
        backgroundColor: const Color.fromARGB(255, 77, 70, 153),
      ),
      ),
      body: Stack(
        children: [
          // Background image
          Container(
            decoration: BoxDecoration(
              image: DecorationImage(
                image: AssetImage("assets/plain sky background.png"), // Your background image
                fit: BoxFit.fill,
              ),
            ),
          ),
          // Content on top of background
          SingleChildScrollView(
            child: Padding(
              padding: const EdgeInsets.all(20.0),
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.center,
                children: [
                  // Game Levels Header
                  Text(
                    'Progress Tracker',
                    style: TextStyle(
                      fontSize: 32,
                      color: const Color.fromARGB(255, 0, 0, 0),
                      fontWeight: FontWeight.bold,
                    ),
                  ),
                  SizedBox(height: 20), // Space between the header and progress bars
                  
                  Center(
                    child: Container(
                      padding: const EdgeInsets.all(20.0),
                      decoration:BoxDecoration(
                        color: Colors.white.withOpacity(0.8), 
                        borderRadius: BorderRadius.circular(15), 
                        boxShadow: [
                          BoxShadow(
                            color:Colors.black.withOpacity(0.2),
                            spreadRadius:3,
                            blurRadius:5,
                            offset:Offset(0,3),
                          ),
                        ]
                      ),
                      child:Column(
                        crossAxisAlignment: CrossAxisAlignment.start,
                        children: [
      
                    _buildLevelProgress('Level 1', 0.5, context),
                    SizedBox(height: 20), // Space between levels
                    
                    // Level 2
                    _buildLevelProgress('Level 2', 0, context),
                    SizedBox(height: 20), // Space between levels
                    
                    // Level 3
                    _buildLevelProgress('Level 3', 0, context),
                    SizedBox(height: 20), // Space between levels
                    
                    // Add more levels as needed
                    // Level 4
                    _buildLevelProgress('Level 4', 0, context),
                        ]
                      ),
                                  ),
                  ),
              ],
          ), 
          ),
    ),
    ]));
     
  
  }

  // Helper method to create a progress bar for each level
  Widget _buildLevelProgress(String levelTitle, double progress, BuildContext context) {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        Text(
          levelTitle,
          style: TextStyle(
            fontSize: 20,
            color: Colors.black,
            fontWeight: FontWeight.w600,
          ),
        ),
        SizedBox(height: 10 ),
        Stack(
          clipBehavior: Clip.none,
          children: [
            // Progress background
            Container(
              width: MediaQuery.of(context).size.width * 0.8,
              height: 20,
              decoration: BoxDecoration(
                borderRadius: BorderRadius.circular(10),
                color: const Color.fromARGB(255, 255, 255, 255),
              ),
            ),
            // Actual progress bar
            Container(
              width: MediaQuery.of(context).size.width * 0.8 * progress,
              height: 20,
              decoration: BoxDecoration(
                borderRadius: BorderRadius.circular(10),
                color: const Color.fromARGB(255, 77, 70, 153), // Adjust progress bar color
              ),
            ),
            //snowball
            Positioned(
              left: MediaQuery.of(context).size.width*0.8*progress - 15,
              top: -10,
              child: Image.asset('assets/snowball.png', 
              width:40,
              height:40,
              ),
              ),
          ],
        ),
      ],
    );
  }
}