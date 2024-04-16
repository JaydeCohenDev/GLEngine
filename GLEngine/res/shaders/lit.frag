#version 330 core
in vec3 vertexColor;
in vec2 texCoord;
in vec3 normal;

out vec4 OutputColor;

uniform vec3 objectColor;
uniform vec3 lightColor;
//uniform vec3 lightPos;

void main()
{
    float ambientStrength = 0.1;
    vec3 ambient = ambientStrength * lightColor;
    
    vec3 result = ambient * objectColor;
    OutputColor = vec4(normal, 1.0);
} 