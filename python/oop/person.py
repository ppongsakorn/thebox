import math
class Person:
    #static member share between instance
    Gender = "G"
    FirstName = "F"
    LastName = "L"
    Info = "I"

    #default constructor
    def __init__(self,gender="male",firstname="none",lastname="none"):
        self.Gender = gender
        self.FirstName = firstname
        self.LastName = lastname
        self.Info = "General Person"

    #override __str__
    def __str__(self):
        return "This is {0}".format(self.Info)

    def run(self):
        print("Run")
    
    def showinfo(self):
        print(self.Gender)
        print(self.FirstName)
        print(self.LastName)
'''
p = Person("Male","Pongsakorn","Poosankam")

p.__str__()
p.run()
p.showinfo()
print(Person.Gender)
print(Person.FirstName)
print(Person.LastName)
p.Gender = "MM"
Person.Gender = "NN"
print(p.Gender)
print(Person.Gender)
'''

class Student(Person):
    def __init__(self, gender, firstname, lastname):
        super().__init__(gender=gender, firstname=firstname, lastname=lastname)
    
    def __str__(self):
        return super().__str__()

std = Student("Female","Jane","February")
dir(math)