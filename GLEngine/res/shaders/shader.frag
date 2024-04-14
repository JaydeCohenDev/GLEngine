#version 330 core
in vec3 vertexColor;
in vec2 texCoord;
out vec4 OutputColor;

uniform sampler2D texture1;
uniform sampler2D texture2;

void main()
{
    OutputColor = mix(texture(texture1, texCoord), texture(texture2, texCoord), 0.2);
    //OutputColor = texture(texture0, texCoord);
    //FragColor = vec4(texCoord, 0.0, 1.0);
} 