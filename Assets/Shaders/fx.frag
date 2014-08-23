#version 120
uniform sampler2D texture;
uniform sampler2D noiseTexture;
uniform sampler2D gradientMap;
uniform vec4 overlayColor;
uniform vec4 additiveColor;
uniform vec4 subtractiveColor;
uniform float overlayNoise;
uniform float inverted;
uniform float noiseX;
uniform float noiseY;
uniform float gradientMapMix;
uniform float gradientMapOffset;
 
void main() {
	//get color of this pixel
	vec2 pixpos = gl_TexCoord[0].xy;
	vec4 pixcol = texture2D(texture, pixpos);
 
	vec4 outcol = abs(inverted-pixcol);
	outcol.a = pixcol.a;
 
	float gray = (outcol.r + outcol.g + outcol.b) / 3;
 
	vec4 gradientColor = texture2D(gradientMap, vec2(gray, gradientMapOffset));
	outcol = mix(outcol, gradientColor, gradientMapMix);
 
	//mix in the overlay color
	outcol = mix(outcol, overlayColor, overlayColor.a);
 
	//add the additive color
	outcol += additiveColor * additiveColor.a;
 
	//subtract the subtractive color (DUH)
	outcol -= subtractiveColor;
 
	vec4 noiseColor = texture2D(noiseTexture, mod(pixpos + vec2(noiseX, noiseY), 1));
	outcol = mix(outcol, noiseColor, overlayNoise);
 
	//reset alpha to prevent coloring transparent pixels
	outcol.a = pixcol.a;
 
	//output the final color
	gl_FragColor = outcol * gl_Color;
}