[33mcommit 1370c2a91b530a91014e7819d3d63ed6c6131de4[m
Author: martin.mirchev <marti_2203@abv.bg>
Date:   Wed May 2 22:55:29 2018 +0300

    * .gitignore: Created Arithmetic Module. Starting Object Hierarchy.
      Created Symbol and the most important objects.
    
    * Kernel.sln:
    * Object.cs:
    * Symbol.cs:
    * Program.cs:
    * IEvaluate.cs:
    * Operative.cs:
    * Kernel.csproj:
    * Applicative.cs:
    * Environment.cs:
    * Continuation.cs:
    * Real.cs:
    * Complex.cs:
    * NoBindingException.cs:
    * Rational.cs:
    * AssemblyInfo.cs:

[1mdiff --git a/.gitignore b/.gitignore[m
[1mnew file mode 100644[m
[1mindex 0000000..4e82d27[m
[1m--- /dev/null[m
[1m+++ b/.gitignore[m
[36m@@ -0,0 +1,40 @@[m
[32m+[m[32m# Autosave files[m[41m[m
[32m+[m[32m*~[m[41m[m
[32m+[m[41m[m
[32m+[m[32m# build[m[41m[m
[32m+[m[32m[Oo]bj/[m[41m[m
[32m+[m[32m[Bb]in/[m[41m[m
[32m+[m[32mpackages/[m[41m[m
[32m+[m[32mTestResults/[m[41m[m
[32m+[m[41m[m
[32m+[m[32m# globs[m[41m[m
[32m+[m[32mMakefile.in[m[41m[m
[32m+[m[32m*.DS_Store[m[41m[m
[32m+[m[32m*.sln.cache[m[41m[m
[32m+[m[32m*.suo[m[41m[m
[32m+[m[32m*.cache[m[41m[m
[32m+[m[32m*.pidb[m[41m[m
[32m+[m[32m*.userprefs[m[41m[m
[32m+[m[32m*.usertasks[m[41m[m
[32m+[m[32mconfig.log[m[41m[m
[32m+[m[32mconfig.make[m[41m[m
[32m+[m[32mconfig.status[m[41m[m
[32m+[m[32maclocal.m4[m[41m[m
[32m+[m[32minstall-sh[m[41m[m
[32m+[m[32mautom4te.cache/[m[41m[m
[32m+[m[32m*.user[m[41m[m
[32m+[m[32m*.tar.gz[m[41m[m
[32m+[m[32mtarballs/[m[41m[m
[32m+[m[32mtest-results/[m[41m[m
[32m+[m[32mThumbs.db[m[41m[m
[32m+[m[41m[m
[32m+[m[32m# Mac bundle stuff[m[41m[m
[32m+[m[32m*.dmg[m[41m[m
[32m+[m[32m*.app[m[41m[m
[32m+[m[41m[m
[32m+[m[32m# resharper[m[41m[m
[32m+[m[32m*_Resharper.*[m[41m[m
[32m+[m[32m*.Resharper[m[41m[m
[32m+[m[41m[m
[32m+[m[32m# dotCover[m[41m[m
[32m+[m[32m*.dotCover[m[41m[m
[1mdiff --git a/Kernel.sln b/Kernel.sln[m
[1mnew file mode 100644[m
[1mindex 0000000..d060988[m
[1m--- /dev/null[m
[1m+++ b/Kernel.sln[m
[36m@@ -0,0 +1,17 @@[m
[32m+[m[41m[m
[32m+[m[32mMicrosoft Visual Studio Solution File, Format Version 12.00[m[41m[m
[32m+[m[32m# Visual Studio 2012[m[41m[m
[32m+[m[32mProject("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "Kernel", "Kernel\Kernel.csproj", "{83FF2FB9-A8D3-4636-B179-5B2BB65D48F9}"[m[41m[m
[32m+[m[32mEndProject[m[41m[m
[32m+[m[32mGlobal[m[41m[m
[32m+[m	[32mGlobalSection(SolutionConfigurationPlatforms) = preSolution[m[41m[m
[32m+[m		[32mDebug|x86 = Debug|x86[m[41m[m
[32m+[m		[32mRelease|x86 = Release|x86[m[41m[m
[32m+[m	[32mEndGlobalSection[m[41m[m
[32m+[m	[32mGlobalSection(ProjectConfigurationPlatforms) = postSolution[m[41m[m
[32m+[m		[32m{83FF2FB9-A8D3-4636-B179-5B2BB65D48F9}.Debug|x86.ActiveCfg = Debug|x86[m[41m[m
[32m+[m		[32m{83FF2FB9-A8D3-4636-B179-5B2BB65D48F9}.Debug|x86.Build.0 = Debug|x86[m[41m[m
[32m+[m		[32m{83FF2FB9-A8D3-4636-B179-5B2BB65D48F9}.Release|x86.ActiveCfg = Release|x86[m[41m[m
[32m+[m		[32m{83FF2FB9-A8D3-4636-B179-5B2BB65D48F9}.Release|x86.Build.0 = Release|x86[m[41m[m
[32m+[m	[32mEndGlobalSection[m[41m[m
[32m+[m[32mEndGlobal[m[41m[m
[1mdiff --git a/Kernel/Applicative.cs b/Kernel/Applicative.cs[m
[1mnew file mode 100644[m
[1mindex 0000000..00092b2[m
[1m--- /dev/null[m
[1m+++ b/Kernel/Applicative.cs[m
[36m@@ -0,0 +1,10 @@[m
[32m+[m[32m﻿using System;[m
[32m+[m[32mnamespace Kernel[m
[32m+[m[32m{[m
[32m+[m[32m    public class Applicative :Object[m
[32m+[m[32m    {[m
[32m+[m[32m        public Applicative()[m
[32m+[m[32m        {[m
[32m+[m[32m        }[m
[32m+[m[32m    }[m
[32m+[m[32m}[m
[1mdiff --git a/Kernel/Arithmetic/Complex.cs b/Kernel/Arithmetic/Complex.cs[m
[1mnew file mode 100644[m
[1mindex 0000000..2a7944a[m
[1m--- /dev/null[m
[1m+++ b/Kernel/Arithmetic/Complex.cs[m
[36m@@ -0,0 +1,12 @@[m
[32m+[m[32m﻿using System;[m
[32m+[m[32mnamespace Kernel.Arithmetic[m
[32m+[m[32m{[m
[32m+[m[32m    public class Complex : Object[m
[32m+[m[32m    {[m
[32m+[m[32m        public decimal Real { get; set; }[m
[32m+[m[32m        public decimal Fractional { get; set; }[m
[32m+[m[32m        public Complex()[m
[32m+[m[32m        {[m
[32m+[m[32m        }[m
[32m+[m[32m    }[m
[32m+[m[32m}[m
[1mdiff --git a/Kernel/Arithmetic/Rational.cs b/Kernel/Arithmetic/Rational.cs[m
[1mnew file mode 100644[m
[1mindex 0000000..dde7fbf[m
[1m--- /dev/null[m
[1m+++ b/Kernel/Arithmetic/Rational.cs[m
[36m@@ -0,0 +1,10 @@[m
[32m+[m[32m﻿using System;[m
[32m+[m[32mnamespace Kernel.Arithmetic[m
[32m+[m[32m{[m
[32m+[m[32m    public class Rational : Real[m
[32m+[m[32m    {[m
[32m+[m[32m        public Rational()[m
[32m+[m[32m        {[m
[32m+[m[32m        }[m
[32m+[m[32m    }[m
[32m+[m[32m}[m
[1mdiff --git a/Kernel/Arithmetic/Real.cs b/Kernel/Arithmetic/Real.cs[m
[1mnew file mode 100644[m
[1mindex 0000000..350ecff[m
[1m--- /dev/null[m
[1m+++ b/Kernel/Arithmetic/Real.cs[m
[36m@@ -0,0 +1,12 @@[m
[32m+[m[32m﻿using System;[m
[32m+[m[32mnamespace Kernel.Arithmetic[m
[32m+[m[32m{[m
[32m+[m[32m    public class Real : Complex[m
[32m+[m[32m    {[m
[32m+[m[32m        private new decimal Fractional => 0.0M;[m
[32m+[m[32m        public Real()[m
[32m+[m[32m        {[m
[32m+[m[41m            [m
[32m+[m[32m        }[m
[32m+[m[32m    }[m
[32m+[m[32m}[m
[1mdiff --git a/Kernel/Continuation.cs b/Kernel/Continuation.cs[m
[1mnew file mode 100644[m
[1mindex 0000000..47aa957[m
[1m--- /dev/null[m
[1m+++ b/Kernel/Continuation.cs[m
[36m@@ -0,0 +1,10 @@[m
[32m+[m[32m﻿using System;[m
[32m+[m[32mnamespace Kernel[m
[32m+[m[32m{[m
[32m+[m[32m    public class Continuation : Object[m
[32m+[m[32m    {[m
[32m+[m[32m        public Continuation()[m
[32m+[m[32m        {[m
[32m+[m[32m        }[m
[32m+[m[32m    }[m
[32m+[m[32m}[m
[1mdiff --git a/Kernel/Environment.cs b/Kernel/Environment.cs[m
[1mnew file mode 100644[m
[1mindex 0000000..9bf3d02[m
[1m--- /dev/null[m
[1m+++ b/Kernel/Environment.cs[m
[36m@@ -0,0 +1,25 @@[m
[32m+[m[32m﻿using System;[m
[32m+[m[32musing System.Collections.Generic;[m
[32m+[m[32mnamespace Kernel[m
[32m+[m[32m{[m
[32m+[m[32m    public class Environment[m
[32m+[m[32m    {[m
[32m+[m[32m        public static readonly Environment empty = new Environment();[m
[32m+[m[32m        public Environment parent;[m
[32m+[m[32m        readonly IDictionary<string, Object> bindings = new Dictionary<string, Object>();[m
[32m+[m
[32m+[m[32m        Environment()[m
[32m+[m[32m        {[m
[32m+[m
[32m+[m[32m        }[m
[32m+[m
[32m+[m[32m        public Environment(Environment parent)[m
[32m+[m[32m        {[m
[32m+[m[32m            this.parent = parent;[m
[32m+[m[32m        }[m
[32m+[m[32m        public Object this[string name][m
[32m+[m[32m        => bindings.ContainsKey(name) ? bindings[name][m
[32m+[m[32m                            : parent == null ? throw new NoBindingException("No Binding for " + name)[m
[32m+[m[32m                      : parent[name];[m
[32m+[m[32m    }[m
[32m+[m[32m}[m
[1mdiff --git a/Kernel/IEvaluate.cs b/Kernel/IEvaluate.cs[m
[1mnew file mode 100644[m
[1mindex 0000000..26e9b4b[m
[1m--- /dev/null[m
[1m+++ b/Kernel/IEvaluate.cs[m
[36m@@ -0,0 +1,8 @@[m
[32m+[m[32m﻿using System;[m
[32m+[m[32mnamespace Kernel[m
[32m+[m[32m{[m
[32m+[m[32m    public interface IEvaluate[m
[32m+[m[32m    {[m
[32m+[m[32m        void Evaluate();[m
[32m+[m[32m    }[m
[32m+[m[32m}[m
[1mdiff --git a/Kernel/Kernel.csproj b/Kernel/Kernel.csproj[m
[1mnew file mode 100644[m
[1mindex 0000000..4d1d2c9[m
[1m--- /dev/null[m
[1m+++ b/Kernel/Kernel.csproj[m
[36m@@ -0,0 +1,52 @@[m
[32m+[m[32m<?xml version="1.0" encoding="utf-8"?>[m[41m[m
[32m+[m[32m<Project DefaultTargets="Build" ToolsVersion="4.0" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">[m[41m[m
[32m+[m[32m  <PropertyGroup>[m[41m[m
[32m+[m[32m    <Configuration Condition=" '$(Configuration)' == '' ">Debug</Configuration>[m[41m[m
[32m+[m[32m    <Platform Condition=" '$(Platform)' == '' ">x86</Platform>[m[41m[m
[32m+[m[32m    <ProjectGuid>{83FF2FB9-A8D3-4636-B179-5B2BB65D48F9}</ProjectGuid>[m[41m[m
[32m+[m[32m    <OutputType>Exe</OutputType>[m[41m[m
[32m+[m[32m    <RootNamespace>Kernel</RootNamespace>[m[41m[m
[32m+[m[32m    <AssemblyName>Kernel</AssemblyName>[m[41m[m
[32m+[m[32m    <TargetFrameworkVersion>v4.6.1</TargetFrameworkVersion>[m[41m[m
[32m+[m[32m  </PropertyGroup>[m[41m[m
[32m+[m[32m  <PropertyGroup Condition=" '$(Configuration)|$(Platform)' == 'Debug|x86' ">[m[41m[m
[32m+[m[32m    <DebugSymbols>true</DebugSymbols>[m[41m[m
[32m+[m[32m    <DebugType>full</DebugType>[m[41m[m
[32m+[m[32m    <Optimize>false</Optimize>[m[41m[m
[32m+[m[32m    <OutputPath>bin\Debug</OutputPath>[m[41m[m
[32m+[m[32m    <DefineConstants>DEBUG;</DefineConstants>[m[41m[m
[32m+[m[32m    <ErrorReport>prompt</ErrorReport>[m[41m[m
[32m+[m[32m    <WarningLevel>4</WarningLevel>[m[41m[m
[32m+[m[32m    <ExternalConsole>true</ExternalConsole>[m[41m[m
[32m+[m[32m    <PlatformTarget>x86</PlatformTarget>[m[41m[m
[32m+[m[32m  </PropertyGroup>[m[41m[m
[32m+[m[32m  <PropertyGroup Condition=" '$(Configuration)|$(Platform)' == 'Release|x86' ">[m[41m[m
[32m+[m[32m    <Optimize>true</Optimize>[m[41m[m
[32m+[m[32m    <OutputPath>bin\Release</OutputPath>[m[41m[m
[32m+[m[32m    <ErrorReport>prompt</ErrorReport>[m[41m[m
[32m+[m[32m    <WarningLevel>4</WarningLevel>[m[41m[m
[32m+[m[32m    <ExternalConsole>true</ExternalConsole>[m[41m[m
[32m+[m[32m    <PlatformTarget>x86</PlatformTarget>[m[41m[m
[32m+[m[32m  </PropertyGroup>[m[41m[m
[32m+[m[32m  <ItemGroup>[m[41m[m
[32m+[m[32m    <Reference Include="System" />[m[41m[m
[32m+[m[32m  </ItemGroup>[m[41m[m
[32m+[m[32m  <ItemGroup>[m[41m[m
[32m+[m[32m    <Compile Include="Program.cs" />[m[41m[m
[32m+[m[32m    <Compile Include="Properties\AssemblyInfo.cs" />[m[41m[m
[32m+[m[32m    <Compile Include="Environment.cs" />[m[41m[m
[32m+[m[32m    <Compile Include="Object.cs" />[m[41m[m
[32m+[m[32m    <Compile Include="NoBindingException.cs" />[m[41m[m
[32m+[m[32m    <Compile Include="Symbol.cs" />[m[41m[m
[32m+[m[32m    <Compile Include="Applicative.cs" />[m[41m[m
[32m+[m[32m    <Compile Include="Operative.cs" />[m[41m[m
[32m+[m[32m    <Compile Include="Continuation.cs" />[m[41m[m
[32m+[m[32m    <Compile Include="Arithmetic\Complex.cs" />[m[41m[m
[32m+[m[32m    <Compile Include="Arithmetic\Real.cs" />[m[41m[m
[32m+[m[32m    <Compile Include="Arithmetic\Rational.cs" />[m[41m[m
[32m+[m[32m  </ItemGroup>[m[41m[m
[32m+[m[32m  <ItemGroup>[m[41m[m
[32m+[m[32m    <Folder Include="Arithmetic\" />[m[41m[m
[32m+[m[32m  </ItemGroup>[m[41m[m
[32m+[m[32m  <Import Project="$(MSBuildBinPath)\Microsoft.CSharp.targets" />[m[41m[m
[32m+[m[32m</Project>[m
\ No newline at end of file[m
[1mdiff --git a/Kernel/NoBindingException.cs b/Kernel/NoBindingException.cs[m
[1mnew file mode 100644[m
[1mindex 0000000..b4ba103[m
[1m--- /dev/null[m
[1m+++ b/Kernel/NoBindingException.cs[m
[36m@@ -0,0 +1,14 @@[m
[32m+[m[32m﻿using System;[m
[32m+[m[32mnamespace Kernel[m
[32m+[m[32m{[m
[32m+[m[32m    public class NoBindingException : InvalidOperationException[m
[32m+[m[32m    {[m
[32m+[m[32m        public NoBindingException()[m[41m         [m
[32m+[m[32m        {[m
[32m+[m[32m        }[m
[32m+[m[32m        public NoBindingException(string message) :base(message)[m
[32m+[m[32m        {[m
[32m+[m[41m            [m
[32m+[m[32m        }[m
[32m+[m[32m    }[m
[32m+[m[32m}[m
[1mdiff --git a/Kernel/Object.cs b/Kernel/Object.cs[m
[1mnew file mode 100644[m
[1mindex 0000000..9f8c2ad[m
[1m--- /dev/null[m
[1m+++ b/Kernel/Object.cs[m
[36m@@ -0,0 +1,15 @@[m
[32m+[m[32m﻿using System;[m
[32m+[m[32mnamespace Kernel[m
[32m+[m[32m{[m
[32m+[m[32m    public class Object[m
[32m+[m[32m    {[m
[32m+[m[32m        public Object()[m
[32m+[m[32m        {[m
[32m+[m[32m        }[m
[32m+[m
[32m+[m[32m        public virtual void Evaluate()[m
[32m+[m[32m        {[m
[32m+[m[41m            [m
[32m+[m[32m        }[m
[32m+[m[32m    }[m
[32m+[m[32m}[m
[1mdiff --git a/Kernel/Operative.cs b/Kernel/Operative.cs[m
[1mnew file mode 100644[m
[1mindex 0000000..d70f18a[m
[1m--- /dev/null[m
[1m+++ b/Kernel/Operative.cs[m
[36m@@ -0,0 +1,10 @@[m
[32m+[m[32m﻿using System;[m
[32m+[m[32mnamespace Kernel[m
[32m+[m[32m{[m
[32m+[m[32m    public class Operative : Object[m
[32m+[m[32m    {[m
[32m+[m[32m        public Operative()[m
[32m+[m[32m        {[m
[32m+[m[32m        }[m
[32m+[m[32m    }[m
[32m+[m[32m}[m
[1mdiff --git a/Kernel/Program.cs b/Kernel/Program.cs[m
[1mnew file mode 100644[m
[1mindex 0000000..dbd3fe3[m
[1m--- /dev/null[m
[1m+++ b/Kernel/Program.cs[m
[36m@@ -0,0 +1,13 @@[m
[32m+[m[32m﻿using System;[m
[32m+[m[32musing static System.Console;[m
[32m+[m[32mnamespace Kernel[m
[32m+[m[32m{[m
[32m+[m[32m    class Program[m
[32m+[m[32m    {[m
[32m+[m[32m        public static void Main(string[] args)[m
[32m+[m[32m        {[m
[32m+[m[32m            while (true)[m
[32m+[m[32m                ReadLine();[m
[32m+[m[32m        }[m
[32m+[m[32m    }[m
[32m+[m[32m}[m
[1mdiff --git a/Kernel/Properties/AssemblyInfo.cs b/Kernel/Properties/AssemblyInfo.cs[m
[1mnew file mode 100644[m
[1mindex 0000000..ccca0f8[m
[1m--- /dev/null[m
[1m+++ b/Kernel/Properties/AssemblyInfo.cs[m
[36m@@ -0,0 +1,26 @@[m
[32m+[m[32m﻿using System.Reflection;[m
[32m+[m[32musing System.Runtime.CompilerServices;[m
[32m+[m
[32m+[m[32m// Information about this assembly is defined by the following attributes.[m[41m [m
[32m+[m[32m// Change them to the values specific to your project.[m
[32m+[m
[32m+[m[32m[assembly: AssemblyTitle("Kernel")][m
[32m+[m[32m[assembly: AssemblyDescription("")][m
[32m+[m[32m[assembly: AssemblyConfiguration("")][m
[32m+[m[32m[assembly: AssemblyCompany("")][m
[32m+[m[32m[assembly: AssemblyProduct("")][m
[32m+[m[32m[assembly: AssemblyCopyright("")][m
[32m+[m[32m[assembly: AssemblyTrademark("")][m
[32m+[m[32m[assembly: AssemblyCulture("")][m
[32m+[m
[32m+[m[32m// The assembly version has the format "{Major}.{Minor}.{Build}.{Revision}".[m
[32m+[m[32m// The form "{Major}.{Minor}.*" will automatically update the build and revision,[m
[32m+[m[32m// and "{Major}.{Minor}.{Build}.*" will update just the revision.[m
[32m+[m
[32m+[m[32m[assembly: AssemblyVersion("1.0.*")][m
[32m+[m
[32m+[m[32m// The following attributes are used to specify the signing key for the assembly,[m[41m [m
[32m+[m[32m// if desired. See the Mono documentation for more information about signing.[m
[32m+[m
[32m+[m[32m//[assembly: AssemblyDelaySign(false)][m
[32m+[m[32m//[assembly: AssemblyKeyFile("")][m
[1mdiff --git a/Kernel/Symbol.cs b/Kernel/Symbol.cs[m
[1mnew file mode 100644[m
[1mindex 0000000..cdfd9ea[m
[1m--- /dev/null[m
[1m+++ b/Kernel/Symbol.cs[m
[36m@@ -0,0 +1,18 @@[m
[32m+[m[32m﻿using System;[m
[32m+[m[32mnamespace Kernel[m
[32m+[m[32m{[m
[32m+[m[32m    public class Symbol : Object[m
[32m+[m[32m    {[m
[32m+[m[32m        string data;[m
[32m+[m
[32m+[m[32m        public Symbol(string data)[m
[32m+[m[32m        {[m
[32m+[m[32m            this.data = data;[m
[32m+[m[32m        }[m
[32m+[m
[32m+[m[32m        public override void Evaluate()[m
[32m+[m[32m        {[m
[32m+[m[41m            [m
[32m+[m[32m        }[m
[32m+[m[32m    }[m
[32m+[m[32m}[m
