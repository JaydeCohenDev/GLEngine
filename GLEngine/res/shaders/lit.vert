#version 330 core
layout (location = 0) in vec3 aPos; 
layout (location = 1) in vec2 aTexCoord;
layout (location = 2) in vec3 aColor; 
layout (location = 3) in vec3 aNormal;

out vec2 texCoord;
out vec3 vertexColor; 
out vec3 normal;
out vec3 fragPos;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main()
{
    gl_Position = vec4(aPos, 1.0) * model * view * projection;
    fragPos = vec3(model * vec4(aPos, 1.0));
    texCoord = aTexCoord;
    vertexColor = aColor;
    normal = aNormal * mat3(transpose(inverse(model)));
}