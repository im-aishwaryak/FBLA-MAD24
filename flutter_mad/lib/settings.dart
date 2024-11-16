import 'package:flutter/material.dart';

class SettingsPage extends StatefulWidget {
  @override
  // ignore: library_private_types_in_public_api
  _SettingsPageState createState() => _SettingsPageState();
}

class _SettingsPageState extends State<SettingsPage> {
  final TextEditingController _bugReportController = TextEditingController();

  @override
  void dispose() {
    _bugReportController.dispose();
    super.dispose();
  }

  void _submitBugReport() {
    String bugDescription = _bugReportController.text.trim();

    if (bugDescription.isEmpty) {
      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(content: Text('Please enter a bug description.')),
      );
      return;
    }

    // Here, you can integrate your backend or email service for bug reporting.
    // For now, we just clear the input and show a confirmation message.
    _bugReportController.clear();
    ScaffoldMessenger.of(context).showSnackBar(
      SnackBar(content: Text('Bug report submitted. Thank you!')),
    );
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Settings'),
        centerTitle: true,
      ),
      body: SingleChildScrollView(
        padding: EdgeInsets.all(16.0),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            // Instructions Section
            Text(
              'How to Play SnowPeak Secrets',
              style: TextStyle(
                fontSize: 20,
                fontWeight: FontWeight.bold,
              ),
            ),
            SizedBox(height: 10),
            Text(
              '1. Answer questions to climb the mountain. \n'
              '2. Correct answers earn points and help you ascend faster.\n'
              '3. Once you reach the top, switch to running mode and dodge obstacles to reach the peak!\n'
              '4. Compete with friends for the highest score.',
              style: TextStyle(fontSize: 16),
            ),
            Divider(height: 30, thickness: 1),

            // Bug Report Section
            Text(
              'Report a Bug',
              style: TextStyle(
                fontSize: 20,
                fontWeight: FontWeight.bold,
              ),
            ),
            SizedBox(height: 10),
            Text(
              'If you encounter any issues while playing the game, please describe them below. Your feedback helps us improve the game!',
              style: TextStyle(fontSize: 16),
            ),
            SizedBox(height: 20),
            TextField(
              controller: _bugReportController,
              maxLines: 5,
              decoration: InputDecoration(
                border: OutlineInputBorder(),
                labelText: 'Describe the bug...',
              ),
            ),
            SizedBox(height: 20),
            ElevatedButton(
              onPressed: _submitBugReport,
              style: ElevatedButton.styleFrom(
                padding: EdgeInsets.symmetric(vertical: 12.0, horizontal: 20.0),
                textStyle: TextStyle(fontSize: 16),
              ),
              child: Text('Submit Bug Report'),
            ),
          ],
        ),
      ),
    );
  }
}
