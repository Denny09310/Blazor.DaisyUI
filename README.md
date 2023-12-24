<!-- Improved compatibility of back to top link: See: https://github.com/othneildrew/Best-README-Template/pull/73 -->
<a name="readme-top"></a>

<!-- PROJECT SHIELDS -->
<!--
*** I'm using markdown "reference style" links for readability.
*** Reference links are enclosed in brackets [ ] instead of parentheses ( ).
*** See the bottom of this document for the declaration of the reference variables
*** for contributors-url, forks-url, etc. This is an optional, concise syntax you may use.
*** https://www.markdownguide.org/basic-syntax/#reference-style-links
-->
[![Contributors][contributors-shield]][contributors-url] [![Forks][forks-shield]][forks-url] [![Stargazers][stars-shield]][stars-url] [![Issues][issues-shield]][issues-url] [![MIT License][license-shield]][license-url]

<!-- PROJECT LOGO -->
<br />
<div align="center">
  <a href="https://github.com/Denny09310/Blazor.DaisyUI">
    <img src="blazor-logo.png" alt="Blazor Logo" width="80" height="80">
    <img src="daisy-ui-logo.png" alt="DaisyUI Logo" width="80" height="80">
  </a>

<h3 align="center">Blazor.DaysUI</h3>

  <p align="center">
    This library, initially designed as a Razor Class Library for Blazor components, 
    draws inspiration from the library and integrates TailwindCSS. 
    A unique approach was taken by converting the project into a "dotnet tool" to address challenges
    arising during the compilation of the NuGet package. Unlike conventional setups, where only ".dll" files are generated, 
    this library ensures the presence of ".razor" files within the source code.
    This adjustment facilitates TailwindCSS in compiling the essential CSS classes embedded in the library. 
    The methodology used is like the technique used by shadcn for TypeScript, 
    allowing for seamless integration and efficient template scaffolding directly within the project.
    <br />
    <a href="https://github.com/Denny09310/Blazor.DaisyUI"><strong>Explore the docs »</strong></a>
    <br />
    <br />
    <a href="https://github.com/Denny09310/Blazor.DaisyUI">View Demo</a>
    ·
    <a href="https://github.com/Denny09310/Blazor.DaisyUI/issues">Report Bug</a>
    ·
    <a href="https://github.com/Denny09310/Blazor.DaisyUI/issues">Request Feature</a>
  </p>
</div>



<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
    <li><a href="#usage">Usage</a></li>
    <li><a href="#roadmap">Roadmap</a></li>
    <li><a href="#contributing">Contributing</a></li>
    <li><a href="#license">License</a></li>
    <li><a href="#contact">Contact</a></li>
    <li><a href="#acknowledgments">Acknowledgments</a></li>
  </ol>
</details>



<!-- ABOUT THE PROJECT -->
## About The Project

[![Product Name Screen Shot][product-screenshot]](https://example.com)

<p align="right">(<a href="#readme-top">back to top</a>)</p>

### Built With

[![Blazor][Blazor]][Blazor-url] [![C#][C#]][C#-url] [![.NET][.NET]][.NET-url]

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- GETTING STARTED -->
## Getting Started

To integrate Blazor.DaisyUI into your Blazor project, follow these simple steps:

NuGet Package Manager Console:

1. **NuGet Package Manager Console:**

```bash
Install-Package Blazor.DaisyUI
```

2. **.NET CLI:**

If you prefer the command line, use the .NET CLI:

```bash
dotnet add package Blazor.DaisyUI
```

Import the namespace inside your <code>_Imports.razor</code>

This will automatically download and install the latest version of Blazor.DaisyUI into your project, along with any necessary dependencies. 

```razor
@using Blazor.DaisyUI
```

and inject the services inside the Dependency Injection Container in <code>Project.cs</code>

```cs
using Blazor.DaisyUI;

builder.Services.AddDaisyUi();
```

Lastly, install the tool to scaffold the templates inside your project

``` bash
dotnet tool install Blazor.DaisyUI.Tool --global
```

<!-- USAGE EXAMPLES -->
## Usage

To scaffold the templates inside your project use the next command, inside the folder with the blazor project

``` bash
daisyui generate alert
```

This will generate the <code>Alert.razor</code> inside your <code>Components/UI</code> folder with the namespace, all the using needed and the required dependencies

_For more examples, please refer to the [Documentation](https://example.com)_

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- ROADMAP -->
## Roadmap

- [ ] Templating all the simple components
- [ ] Templating all the complex components
- [ ] Adding most used custom components


See the [open issues](https://github.com/Denny09310/Blazor.DaisyUI/issues) for a full list of proposed features (and known issues).

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- CONTRIBUTING -->
## Contributing

Contributions are what make the open source community such an amazing place to learn, inspire, and create. Any contributions you make are **greatly appreciated**.

If you have a suggestion that would make this better, please fork the repo and create a pull request. You can also simply open an issue with the tag "enhancement".
Don't forget to give the project a star! Thanks again!

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- LICENSE -->
## License

Distributed under the MIT License. See `LICENSE.txt` for more information.

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- CONTACT -->
## Contact

Koja Dennis - [@Denny093](https://www.instagram.com/Denny093) - k.denny2000@gmail.com

Project Link: [https://github.com/Denny09310/Blazor.DaisyUI](https://github.com/Denny09310/Blazor.DaisyUI)

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- ACKNOWLEDGMENTS -->
## Acknowledgments

* [DaisyUI][daisy-ui-url]
* [Shadcn][shadcn-url]
* [Blazor][Blazor-url]

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- MARKDOWN LINKS & IMAGES -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->
[contributors-shield]: https://img.shields.io/github/contributors/Denny09310/Blazor.DaisyUI.svg?style=for-the-badge
[contributors-url]: https://github.com/Denny09310/Blazor.DaisyUI/graphs/contributors
[forks-shield]: https://img.shields.io/github/forks/Denny09310/Blazor.DaisyUI.svg?style=for-the-badge
[forks-url]: https://github.com/Denny09310/Blazor.DaisyUI/network/members
[stars-shield]: https://img.shields.io/github/stars/Denny09310/Blazor.DaisyUI.svg?style=for-the-badge
[stars-url]: https://github.com/Denny09310/Blazor.DaisyUI/stargazers
[issues-shield]: https://img.shields.io/github/issues/Denny09310/Blazor.DaisyUI.svg?style=for-the-badge
[issues-url]: https://github.com/Denny09310/Blazor.DaisyUI/issues
[license-shield]: https://img.shields.io/github/license/Denny09310/Blazor.DaisyUI.svg?style=for-the-badge
[license-url]: https://github.com/Denny09310/Blazor.DaisyUI/blob/master/LICENSE.txt
[daisy-ui-url]: https://daisyui.com/
[shadcn-url]: https://ui.shadcn.com/
[product-screenshot]: images/screenshot.png
[Blazor]: https://img.shields.io/badge/Blazor-blueviolet?style=for-the-badge&logo=blazor&logoColor=white
[Blazor-url]: https://dotnet.microsoft.com/apps/aspnet/web-apps/blazor
[C#]: https://img.shields.io/badge/C%23-forestgreen?style=for-the-badge&logo=csharp&logoColor=white
[C#-url]: https://docs.microsoft.com/en-us/dotnet/csharp/
[.NET]: https://img.shields.io/badge/.NET-blue?style=for-the-badge&logo=dotnet&logoColor=white
[.NET-url]: https://dotnet.microsoft.com/
