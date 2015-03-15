using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SegmentationImage
{
    class ImageProcessing
    {

        
        // applies erosion to the image
        public byte[] setBinary(byte[] originalImage)
        {
            byte[] setBinary =  new byte[originalImage.Length];
           // setBinary = setGrayscale(originalImage);
            int binaryStep = 127;

            for (int i = 0; i < originalImage.Length; i += 4)
            {
                originalImage[i / 4] = (byte)(0.114 * originalImage[i] + 0.587 * originalImage[i + 1] + 0.299 * originalImage[i + 2]);
            }

             //* TODO: создать новый byte[]
            
                           // TODO: задать порог для бинаризации. по дефолту - 150. потом подобрать.
                          
                         for (int i = 0; i < originalImage.Length; i++)
                          {
                              if (originalImage[i] >= binaryStep)
                              {
                                  setBinary[i] = 255;
                              }
                              else 
                              {
                                  setBinary[i] = 0;
                              }
                          }
                              //* TODO: все пиксле > 150 заменить на белый, иначе - черный
                              //* TODO: прияменять это к черно-белому изображению
             
                 return setBinary;
        }



        // applies erosion to the image
        public byte[] setErosion(byte[] setBinary, int w, int h)
        {
            
            byte[] setErode = new byte[setBinary.Length];
            byte[,] ErodeMatrix = new byte[w, h];
            int k  = 0;
            for(int i = 0; i != h;++i){
                for(int j = 0; j != w;++j){
                    ErodeMatrix[i, j] = setBinary[k];
                    ++k;
                }
            }

            int r = 0;
            for (int i =1; i < w-1; ++i) {
                for (int j = 1; j < h-1; ++j)
                {
         
                    if (ErodeMatrix[i, j] == 255)
                    {
                        ErodeMatrix[i - 1, j - 1] = 0;
                        ErodeMatrix[i + 1, j + 1] = 0;
                        ErodeMatrix[i, j - 1] = 0;
                        ErodeMatrix[i - 1, j] = 0;
                        ErodeMatrix[i + 1, j] = 0;
                        ErodeMatrix[i, j + 1] = 0;
                        ErodeMatrix[i - 1, j + 1] = 0;
                        ErodeMatrix[i + 1, j - 1] = 0;
                    }
                }
                   
                    
                
                }
            /* 
             * TODO: создать новый byte[]
             * public byte[] setGaussianBlur(byte[] ioriginalImage, int w, int h, int coef) {

            byte[] blurImage1 = new byte[ioriginalImage.Length];
            byte[] blurImage2 = new byte[ioriginalImage.Length];

            for (int i = 0; i != ioriginalImage.Length; ++i) { blurImage1[i] = ioriginalImage[i]; };
            int iter = 2;
            for (int i = 0; i != iter; ++i)
            {
                for (int x = 0; x != w; ++x)
                {
                    for (int y = 0; y != h; ++y)
                    {
                        int cur_pixel = x * 4 + y * 4 * w;
                        float r = 0;
                        float g = 0;
                        float b = 0;

                        int count = 0;
                        for (int ix = (x - coef > 0) ? x - coef : 0; (ix <= (x + coef)) && (ix < w); ++ix)
                        {
                            int cur_pixel2 = ix * 4 + y * 4 * w;
                            r += blurImage1[cur_pixel2];
                            g += blurImage1[cur_pixel2 + 1];
                            b += blurImage1[cur_pixel2 + 2];
                            ++count;
                        }
                        blurImage2[cur_pixel] = PixelProcessing.CheckPixelValue((int)(r / count));
                        blurImage2[cur_pixel + 1] = PixelProcessing.CheckPixelValue((int)(g / count));
                        blurImage2[cur_pixel + 2] = PixelProcessing.CheckPixelValue((int)(b / count));
                        blurImage2[cur_pixel + 3] = blurImage1[cur_pixel + 3];
                    }

                }

                for (int x = 0; x != w; ++x)
                {
                    for (int y = 0; y != h; ++y)
                    {
                        int cur_pixel = x * 4 + y * 4 * w;
                        float r = 0;
                        float g = 0;
                        float b = 0;

                        int count = 0;
                        for (int iy = (y - coef > 0) ? y - coef : 0; (iy <= (y + coef)) && (iy < h); ++iy)
                        {
                            int cur_pixel2 = x * 4 + iy * 4 * w;
                            r += blurImage2[cur_pixel2];
                            g += blurImage2[cur_pixel2 + 1];
                            b += blurImage2[cur_pixel2 + 2];
                            ++count;
                        }
                        blurImage1[cur_pixel] = PixelProcessing.CheckPixelValue((int)(r / count));
                        blurImage1[cur_pixel + 1] = PixelProcessing.CheckPixelValue((int)(g / count));
                        blurImage1[cur_pixel + 2] = PixelProcessing.CheckPixelValue((int)(b / count));
                    }

                }
            };
            return blurImage1;
        }
     int b, c;
c = -4;

b = c >= 0 ? c : c*c;   // b = 16
             * TODO: применить операцию эрозии к бинарному изображению
             * структурный элемент - крест 3х3
             */
            return setErode;
        }

        // applies dilatation to the image
        public byte[] setDilatation(byte[] originalImage)
        {
            /* 
             * TODO: создать новый byte[]
             * TODO: применить операцию дилатации к бинарному изображению
             * структурный элемент - кольцо 5х5             
             */
            return null;
        }

        // detectes edges at the image
        public byte[] setEdges(byte[] originalImage)
        {
            /* 
             * TODO: создать новый byte[]
             * TODO: придумать, как выделить края
             * видимо, знания морфологических операций не помешают ^_^
             */
            return null;
        }
    }
}
