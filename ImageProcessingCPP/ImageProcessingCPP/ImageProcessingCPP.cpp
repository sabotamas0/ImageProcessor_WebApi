﻿// ImageProcessingCPP.cpp : Defines the entry point for the application.
//

#include "ImageProcessingCPP.h"
#include <opencv2/opencv.hpp>
#include <thread>
using namespace cv;

using namespace std;

int main(int argc, char** argv)
{
    if (argc != 2)
    {
        printf("usage: DisplayImage.out <Image_Path>\n");
        return -1;
    }
    
    Mat image;
    image = imread(argv[1], IMREAD_COLOR);

    if (!image.data)
    {
        printf("No image data \n");
        return -1;
    }
    namedWindow("Display Image", WINDOW_FULLSCREEN);
    imshow("Display Image", image);

    waitKey(0);

    return 0;
}
