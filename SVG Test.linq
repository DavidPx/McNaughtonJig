<Query Kind="Program">
  <NuGetReference>Svg</NuGetReference>
  <Namespace>Svg</Namespace>
  <Namespace>System.Drawing</Namespace>
</Query>

private const float kerf = 0.01f;

void Main()
{
	var doc = new SvgDocument 
	{
		Width = Mm(200),
		Height = Mm(200),
		
	};

	var c = CutCircle(25, 50, 50);
	
	doc.Children.Add(c);
	doc.Children.Add(CutCircle(25, 150, 50));
	doc.Children.Add(CutCircle(25, 150, 150));
	doc.Children.Add(CutCircle(25, 50, 150));
		
	File.WriteAllText(@"c:\temp\Test.svg", doc.GetXML());		
}

// Define other methods and classes here
private static SvgUnit Mm(float milimeters)
{
	return new SvgUnit(SvgUnitType.Millimeter, milimeters);
}

private static SvgCircle CutCircle(float diameter, float centerX, float centerY)
{
	return new SvgCircle
	{
		Radius = Mm(diameter / 2 - kerf / 2),
		Stroke = Cut(),
		StrokeWidth = Mm(kerf),
		Fill = SvgPaintServer.None,
		CenterX = Mm(centerX),
		CenterY = Mm(centerY)
	};
}

private static SvgColourServer Cut()
{
	return new SvgColourServer(Color.Blue);
}

private static SvgColourServer Etch()
{
	return new SvgColourServer(Color.Red);
}