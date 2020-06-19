all: out.txt
	cat $<

out.txt : B.exe
	mono $< > $@

B.exe : B.cs matrix.dll
	mcs $< -reference:$(word 2, $^)

matrix.dll : ../A/matrix.cs ../vector.cs
	mcs $^ -target:library -out:$@


clean:
	rm -f *.dll *.txt *.exe
