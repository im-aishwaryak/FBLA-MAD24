import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'main.dart'; 

class GameScreen extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    var appState = context.watch<MyAppState>(); 
    return Scaffold(
      appBar: AppBar(
        title: Text(
          'Game!',
          style: TextStyle(
            fontWeight: FontWeight.bold,
            color: Colors.white,
          ),
        ),
        backgroundColor: Colors.teal[800],
      ),
      body: Stack(
        children: [
          // Background image
          Container(
            decoration: BoxDecoration(
              image: DecorationImage(
                image: AssetImage("assets/mountain background.png"), // Your background image
                fit: BoxFit.cover,
              ),
            ),
          ),
          // Button in the center
          Center(
            child: ElevatedButton(
              onPressed: () {
                // Define what happens when the button is pressed
                appState.levelUp(); 
                print('level upp');
                print('${appState.currentLevel}'); 
              },
              style: ElevatedButton.styleFrom(
                padding: EdgeInsets.symmetric(horizontal: 30, vertical: 15),
              ),
              child: Text(
                'Level up',
                style: TextStyle(
                  fontSize: 18,
                  color: Colors.white,
                ),
              ),
            ),
          ),
        ],
      ),
    );
  }
}