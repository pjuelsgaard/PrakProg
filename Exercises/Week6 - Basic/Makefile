CSFLAGS = mcs -warn:1 -optimize-# C#compiler is mcs with warning level 4 (4 is highest) and no optimization
#CSFLAGS = -optimize+ -platform:arm
all : out.txt
	cat out.txt

out.txt : main.exe
	mono $< > $@

main.exe : main.cs 
	mcs $< # The < sign means "first prerequisite" in the out.txt recipe, and can be referred to as such again later



#.PHONEY: clean #If there is a risk that there is already a file called clean
clean: 
	rm -f out.txt main.exe
