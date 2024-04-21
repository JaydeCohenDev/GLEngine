#version 330 core
in vec3 vertexColor;
in vec2 texCoord;
in vec3 normal;
in vec3 fragPos;

out vec4 OutputColor;

uniform vec3 objectColor;
uniform vec3 lightColor;
uniform vec3 lightPos;
uniform vec3 viewPos;

float near = 0.1;
float far  = 100.0;

float LinearizeDepth(float depth)
{
    float z = depth * 2.0 - 1.0; // back to NDC 
    return (2.0 * near * far) / (far + near - z * (far - near));
}

void main()
{
    float ambientStrength = 0.1;
    vec3 ambient = ambientStrength * lightColor;
    
    vec3 norm = normalize(normal);
    vec3 lightDir = normalize(lightPos - fragPos);
    float diff = max(dot(norm, lightDir), 0.0);
    //diff = floor(diff*3) / 3; // Banding
    vec3 diffuse = diff * lightColor;

    //float specularStrength = 0.75;
    float specularStrength = 0.5;
    vec3 viewDir = normalize(viewPos - fragPos);
    vec3 halfwayDir = normalize(lightDir + viewDir);
    vec3 reflectDir = reflect(-lightDir, norm);
//    float spec = pow(max(dot(viewDir, reflectDir), 0.0), 32);
    float spec = pow(max(dot(normal, halfwayDir), 0.0), 32);
    //spec = floor(spec * 2.0) / 2.0;
    vec3 specular = specularStrength * spec * lightColor;
    
    vec3 result = (ambient + diffuse + specular) * objectColor;
    OutputColor = vec4(result, 1.0);

    //float depth = LinearizeDepth(gl_FragCoord.z) / far;
    //OutputColor = vec4(vec3(depth), 1.0);
} 