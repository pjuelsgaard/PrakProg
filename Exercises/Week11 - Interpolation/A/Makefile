
#all : out.txt
#	cat $<

SinPlot.svg : SinPlot.gpi out.txt
	gnuplot $<

out.txt : A.exe
	mono $< > $@

A.exe : A.cs spline.dll
	mcs $< -reference:$(word 2, $^)

spline.dll : spline.cs
	mcs $< -target:library -out:$@

../data.txt : ../sin.exe
	mono $< > $@

../sin.exe : ../sin.cs
	mcs $<

clean:
	rm -f *.txt *.exe *.dll *.svg
