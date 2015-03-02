using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SegmentationImage
{
    class ImageProcessing
    {

        // converts image to gray-scale
       /* private byte[] setGrayscale(byte[] originalImage)
        {
             
             //* TODO: создать новый byte[], который будет создрежать градации серого
            byte[] GrayScale = new byte[originalImage.Length];
             //* TODO: записать в него значения по формуле: x = 0.299R + 0.587G + 0.114B
            for (int i = 0; i < originalImage.Length; i+=4)
            {
                originalImage[i] = (byte)(0.114 * GrayScale[i] + 0.587 * GrayScale[i + 1] + 0.299 * GrayScale[i + 2]);
            }
                //* TODO: вернуть это значение
                //* note: в С# дефолтным сичтается формат Bgra

                return null;
        }
        */
       
        // applies erosion to the image
        public byte[] setBinary(byte[] OriginalImage)
        {             
            
             //* TODO: создать новый byte[]
             byte[] setBinary = new byte[OriginalImage.Length];
             
                 for (int i = 0, j=0; i < OriginalImage.Length; i += 4)
                 {
                     OriginalImage[i] = (byte)(0.114 * OriginalImage[i]);
                     OriginalImage[i + 1] = (byte)(0.587 * OriginalImage[i]);
                     OriginalImage[i + 2] = (byte)(0.299 * OriginalImage[i]);
                     setBinary[j] = (byte)(OriginalImage[i] + OriginalImage[i + 1] + OriginalImage[i + 2]);
                     ++j;
                     
                 }
                           // TODO: задать порог для бинаризации. по дефолту - 150. потом подобрать.
                          int binaryStep = 5;
                          for (int i = 0; i < OriginalImage.Length; i+=4)
                          {
                              if (setBinary[i] >= binaryStep)
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
        public byte[] setErosion(byte[] originalImage)
        {
            /* 
             * TODO: создать новый byte[]
             * TODO: применить операцию эрозии к бинарному изображению
             * структурный элемент - крест 3х3
             */
            return null;
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
