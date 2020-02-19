all: out.txt out-stdin.txt

out.txt : cmdline.exe input.txt Makefile
	mono $< 1 2 3  1> $@    		# > means redirect, with "1>" is standard output
	mono $< $$(cat input.txt) 1>> $@ 	# > is rewrite, >> is append
						# $$ means "let me have my fucking $ sign, so bash can interpret it!"
out-stdin.txt out-stderr.txt: stdin.exe input.txt Makefile
	mono $< < input.txt > $@ 2> out-stderr.txt	# "< bla > ged" connect bla to ged
	$$(cat input.txt) | mono $< >> out-stdin.txt 2>> out-stderr.txt		# is wrong way. | connects stdout of left to stdin of right



cmdline.exe : cmdline.cs
	mcs $<

stdin.exe : stdin.cs
	mcs $<


.PHONEY:
clean :
	$(RM) *.exe out* # $(RM) is the macro for 'rm -f'

input.txt: Makefile
	/bin/echo -e '1.5\n2.5\n3.5\n' > $@
