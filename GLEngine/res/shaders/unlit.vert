﻿#version 330 core
layout (location = 0) in vec3 aPos; // the position variable has attribute position 0
layout (location = 2) in vec2 aTexCoord;
layout (location = 1) in vec3 aColor; // the color variable has attribute position 1

out vec2 texCoord;
out vec3 vertexColor; 

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main()
{
    gl_Position = vec4(aPos, 1.0) * model * view * projection; 
    texCoord = aTexCoord;
    vertexColor = aColor;
}