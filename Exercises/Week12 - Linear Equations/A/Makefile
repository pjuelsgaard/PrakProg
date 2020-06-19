all: out.txt
	cat $<

out.txt : A.exe
	mono $< > $@

A.exe : A.cs matrix.dll
	mcs $< -reference:$(word 2, $^)

matrix.dll : matrix.cs ../vector.cs
	mcs $^ -target:library -out:$@


clean:
	rm -f *.dll *.txt *.exe
