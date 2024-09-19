# APU Programming Café Management System

`ACADEMIC PROJECT`

APU Programming Café is an in-house program to coach APU students who needs additional guidance in programming modules. 

This initiative is to cater students from all levels (Foundation to Degree Level 3).

## Brief

There are three type of coaching class level for every programming module
- Beginner
- Intermediate
- Advance
Module lecturer who is teaching a programming module identifies students who require extra coaching and enrol them into respective coaching class. 

Student may also request to attend for additional coaching classes other than lecturer's recommendation. 

Fee is imposed for every module based on class level. 

Each coaching class is conducted for duration of three months. 

Trainer who is conducting coaching classes, teaches students who had paid fees for his/her classes only.

## Registered Users

1.	Administrator
-	Login - User should key in username and password only. System should identify their user role.
-	Register and delete trainer.
-	Assign trainer to respective level (Beginner, Intermediate, Advance) and modules. A trainer can teach more than one module.
-	View monthly income report based on trainer, level and modules.
-	View feedback sent by trainer.
-	Update own profile.
 
2.	Trainer
-	Login - User should key in username and password only. System should identify their user role.
-	Add coaching class information (e.g. module name, charges, class schedule etc).
-	Update and delete coaching class information.
-	View list of students enrolled and paid for his/her modules.
-	Send feedback (suggestion, complain etc) to administrator.
-	Update own profile.

3.	Lecturer
-	Login - User should key in username and password only. System should identify their user role.
-	Register and enroll student to modules which he/she require coaching. During enrolment, record student information for eg : name, TP number, email, contact number, address, level, modules, month of enrolment etc (you may add other relevant information)
-	Update subject enrollment of a student (for eg: mistakenly enroll into wrong level/module).
-	Approve the request from student to enroll in additional coaching class.
-	Delete student who have completed their coaching class.
-	Update own profile.

4.	Student
-	Login - User should key in username and password only. System should identify their user role.
-	View schedule of his/her coaching classes
-	Send request to lecturer to enroll in additional coaching class for different programming module.
-	Delete request (which is still pending) sent to lecturer to enroll in additional coaching class.
-	View invoice and make payment for modules enrolled.
-	Update own profile.

# Storyboard

![welcome](https://github.com/user-attachments/assets/cd1e4bad-329f-4d7d-a256-54d6d1cc65ab)

| Control             | Description                                                                |
| ----------------- | ------------------------------------------------------------------ |
| PictureBox1 | To display the picture of the welcome page |
| Button 1 | To able user to enter the login page |

![login](https://github.com/user-attachments/assets/926eeb3b-0604-4d14-919a-9ddb4bd7eae3)

| Control             | Description                                                                |
| ----------------- | ------------------------------------------------------------------ |
| Label 1 | To label the related controls |
| Label 2 | To label the related controls |
| TextBox 1 | To allow user to enter their TP Number |
| TextBox 2 | To allow user to enter the password |
| LinkLabel1 | To allow user to get their password when they forgot their password |
| PictureBox1 | To display the icon of the user |
| PictureBox2 | To allow the user to check the password they enter |
| PictureBox3 | To allow the user to exit the login page |

## 🛠 Skills
C#
