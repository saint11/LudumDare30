#version 120
uniform sampler2D texture;
uniform vec4 color;

void main() {
	//get color of this pixel
	vec2 pixpos = gl_TexCoord[0].xy;
	vec4 pixcol = texture2D(texture, pixpos);
	vec4 setColor = color;
 	setColor.a = pixcol.a;
 
 	//output the final color
	gl_FragColor = setColor * gl_Color;
}