﻿using OpenTK.Graphics.OpenGL4;
using StbImageSharp;

namespace GLEngine.RenderCore;

public class Texture
{
    public int Handle { get; protected set; }

    public Texture(string path)
    {
        Handle = GL.GenTexture();
        Use();
        
        // stb_image loads from the top-left pixel, whereas OpenGL loads from the bottom-left, causing the texture to be flipped vertically.
        // This will correct that, making the texture display properly.
        StbImage.stbi_set_flip_vertically_on_load(1);
        
        // Load the image
        ImageResult image = ImageResult.FromStream(File.OpenRead(path), ColorComponents.RedGreenBlueAlpha);
        
        // Set texture data 
        GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, image.Width, image.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, image.Data);
        
        // Mips
        GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
        
        // Texture wrapping
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat); 
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
        
        // Filtering
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
    }

    public void Use(TextureUnit unit = TextureUnit.Texture0)
    {
        GL.ActiveTexture(unit);
        GL.BindTexture(TextureTarget.Texture2D, Handle);
    }
}