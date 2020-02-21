In = 1 2 3 4 5


all: out.txt 
	cat $<

out.txt : cmdline.exe stdin.exe input.txt readfile.exe Makefile
	mono $< $$(cat $(word 3, $^)) > $@
	mono $(word 2, $^) < $(word 3, $^) >> $@
	mono $(word 4, $^) $(word 3, $^) $@
	
cmdline.exe : cmdline.cs
	mcs $<

stdin.exe : stdin.cs
	mcs $<

readfile.exe : readfile.cs
	mcs $<


.PHONEY:
clean :
	$(RM) *.exe out* 

input.txt: Makefile
	/bin/echo -e '1\n2\n3\n4\n5' > $@
