#pragma once
using System::String;
using namespace System::Runtime::InteropServices;

namespace Wrapper {
	public ref class ImageProcessor
	{
	public:
		array<System::Byte>^ Data;
		void ProcessImage(cli::array<unsigned char>^ imageData, String^ mimeType);
	};
}
