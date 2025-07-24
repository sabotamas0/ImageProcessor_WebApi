#include "pch.h"

#include "wrapper.h"

#include <opencv2/core/core.hpp>
#include <opencv2/highgui/highgui.hpp>
#include <opencv2/imgproc/imgproc.hpp>
#include <msclr/marshal_cppstd.h>
#include <omp.h>
using namespace msclr::interop;

void Wrapper::ImageProcessor::ProcessImage(cli::array<unsigned char>^ imageData, String^ mimeType)
{
	pin_ptr<System::Byte> pointerToResource = &imageData[0];
	unsigned char* pointer = pointerToResource;
	cv::Mat img_data(imageData->Length, 1, CV_8U, pointerToResource);

	cv::Mat image = cv::imdecode(img_data, 1);
	int kernelSize = 5;
	int sigmaX = 5;

	#pragma omp parallel for
	for (int i = 0; i < image.rows; ++i)
	{
		cv::GaussianBlur(image.row(i), image.row(i), cv::Size(kernelSize, 5), sigmaX, 0);
	}

	std::vector<unsigned char> bytes;

	std::string extStr = msclr::interop::marshal_as<std::string>(mimeType);

	cv::imencode(extStr, image, bytes);

	Data = gcnew cli::array<System::Byte>(static_cast<int>(bytes.size()));

	#pragma omp parallel for
	for (size_t i = 0; i < bytes.size(); ++i)
	{
		Data[i] = bytes[i];
	}

}