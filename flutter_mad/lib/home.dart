import 'package:flutter/material.dart';
import 'progress_screen.dart'; 
import 'game_screen.dart'; 


class HomePage extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Container(
        decoration: BoxDecoration(
          image: DecorationImage(
            image: AssetImage("assets/home background.png"), // Background image
            fit: BoxFit.fitWidth,
          ),
        ),
        

        child: Row(
          children: [
            // Left side column (text and button)
            Expanded(
              flex: 2,
              child: Column(
                mainAxisAlignment: MainAxisAlignment.end,
                crossAxisAlignment: CrossAxisAlignment.start,
                children: [
                  // Image button below the text
                  Padding(
                    padding: const EdgeInsets.symmetric(horizontal: 50.0, vertical: 70.0),
                    child: InkWell(
                      onTap: () {
                        Navigator.push(
                            context, 
                            MaterialPageRoute(builder: (context) => GameScreen()),
                            );
                      },
                      child: Image.asset(
                        'assets/Your paragraph text (4).png', // Add the path of your new button image
                        width: 120,
                        height: 120,
                      ),
                    ),
                  ),
                ],
              ),
            ),
            // Right side for the row of three image buttons
            Expanded(
              flex: 3,
              child: Column(
                mainAxisAlignment: MainAxisAlignment.center,
                crossAxisAlignment: CrossAxisAlignment.center,
                children: [
                  Row(
                    mainAxisAlignment: MainAxisAlignment.center,
                    children: [
                      // First image button
                      InkWell(
                        onTap: () {
                          print('First image button clicked');
                        },
                        child: Image.asset(
                          'assets/3.png', // Image path
                          width: 120,
                          height: 120,
                        ),
                      ),
                      SizedBox(width: 20), // Spacing between the buttons
                      // Second image button
                      InkWell(
                        onTap: () {
                          Navigator.push(
                            context, 
                            MaterialPageRoute(builder: (context) => ProgressScreen()),
                            );
                        },
                        child: Image.asset(
                          'assets/4.png', // Image path
                          width: 120,
                          height: 120,
                        ),
                      ),
                      SizedBox(width: 20), // Spacing between the buttons
                      // Third image button
                      InkWell(
                        onTap: () {
                          print('Third image button clicked');
                        },
                        child: Image.asset(
                          'assets/5.png', // Image path
                          width: 120,
                          height: 120,
                        ),
                      ),
                    ],
                  ),
                ],
              ),
            ),
          ],
        ),
      ),
    );
  }
}