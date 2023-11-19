# CoreFramework <img src="CFR-128.png" width="48" /> [![Latest Version](https://img.shields.io/github/v/release/litetex/CoreFramework?style=flat-square)](https://github.com/litetex/CoreFramework/releases)
> **Current state**: _Maintenance mode_

Stuff that is frequently used in (my) code and therefore here summarized


<table>
  <thead>
    <th>Module</th>
    <th>Nuget</th>
    <th>Nuget (preview/dev)</th>
    <th>Alternatives</th>
  </thead>
  <tbody>
    <tr>
      <td>Base</td>
      <td>
        <a href="https://www.nuget.org/packages/Litetex.CoreFramework.Base">
          <img alt="Nuget" src="https://img.shields.io/nuget/v/Litetex.CoreFramework.Base?style=flat-square">
        </a>
      </td>
      <td>
        <a href="https://www.nuget.org/packages/Litetex.CoreFramework.Base">
          <img alt="Nuget" src="https://img.shields.io/nuget/vpre/Litetex.CoreFramework.Base?style=flat-square">
        </a>
      </td>
      <td></td>
    </tr>
    <tr>
      <td>Config</td>
      <td>
        <a href="https://www.nuget.org/packages/Litetex.CoreFramework.Config">
          <img alt="Nuget" src="https://img.shields.io/nuget/v/Litetex.CoreFramework.Config?style=flat-square">
        </a>
      </td>
      <td>
        <a href="https://www.nuget.org/packages/Litetex.CoreFramework.Config">
          <img alt="Nuget" src="https://img.shields.io/nuget/vpre/Litetex.CoreFramework.Config?style=flat-square">
        </a>
      </td>
      <td>
        <a href="https://github.com/aloneguid/config">Config.Net</a>
        <a href="https://www.nuget.org/packages/Config.Net">
          <img alt="Nuget" src="https://img.shields.io/nuget/v/Config.Net">
        </a>
      </td>
    </tr>
    <tr>
      <td>Config.Json</td>
      <td>
        <a href="https://www.nuget.org/packages/Litetex.CoreFramework.Config.Json">
          <img alt="Nuget" src="https://img.shields.io/nuget/v/Litetex.CoreFramework.Config.Json?style=flat-square">
        </a>
      </td>
      <td>
        <a href="https://www.nuget.org/packages/Litetex.CoreFramework.Config.Json">
          <img alt="Nuget" src="https://img.shields.io/nuget/vpre/Litetex.CoreFramework.Config.Json?style=flat-square">
        </a>
      </td>
      <td>
        <a href="https://github.com/aloneguid/config#json">Config.Net#JSON</a>
      </td>
    </tr>
    <tr>
      <td>Config.Yaml</td>
      <td>
        <a href="https://www.nuget.org/packages/Litetex.CoreFramework.Config.Yaml">
          <img alt="Nuget" src="https://img.shields.io/nuget/v/Litetex.CoreFramework.Config.Yaml?style=flat-square">
        </a>
      </td>
      <td>
        <a href="https://www.nuget.org/packages/Litetex.CoreFramework.Config.Yaml">
          <img alt="Nuget" src="https://img.shields.io/nuget/vpre/Litetex.CoreFramework.Config.Yaml?style=flat-square">
        </a>
      </td>
      <td>
        <a href="https://github.com/aloneguid/config/tree/master/src/Config.Net.Yaml">Config.Net.Yaml</a>
        <a href="https://www.nuget.org/packages/Config.Net.Yaml">
          <img alt="Nuget" src="https://img.shields.io/nuget/v/Config.Net.Yaml">
        </a>
      </td>
    </tr>
    <tr>
      <td>Logging</td>
      <td>
        <a href="https://www.nuget.org/packages/Litetex.CoreFramework.Logging">
          <img alt="Nuget" src="https://img.shields.io/nuget/v/Litetex.CoreFramework.Logging?style=flat-square">
        </a>
      </td>
      <td>
        <a href="https://www.nuget.org/packages/Litetex.CoreFramework.Logging">
          <img alt="Nuget" src="https://img.shields.io/nuget/vpre/Litetex.CoreFramework.Logging?style=flat-square">
        </a>
      </td>
      <td>
        <a href="https://github.com/serilog/serilog">Serilog</a>
        <a href="https://www.nuget.org/packages/Serilog">
          <img alt="Nuget" src="https://img.shields.io/nuget/v/Serilog">
        </a>
      </td>
    </tr>
    <tr>
      <td>CrashLogging</td>
      <td>
        <a href="https://www.nuget.org/packages/Litetex.CoreFramework.CrashLogging">
          <img alt="Nuget" src="https://img.shields.io/nuget/v/Litetex.CoreFramework.CrashLogging?style=flat-square">
        </a>
      </td>
      <td>
        <a href="https://www.nuget.org/packages/Litetex.CoreFramework.CrashLogging">
          <img alt="Nuget" src="https://img.shields.io/nuget/vpre/Litetex.CoreFramework.CrashLogging?style=flat-square">
        </a>
      </td>
      <td></td>
    </tr>
  </tbody>
</table>


## Development [![Latest Version](https://img.shields.io/github/v/release/litetex/CoreFramework?style=flat-square&include_prereleases&label=prerelease)](https://github.com/litetex/CoreFramework/releases)
| Workflow | Status |
| --- | --- |
| Sonar Build | [![Sonarcloud Status](https://sonarcloud.io/api/project_badges/measure?project=litetex_CoreFrameworkBase&metric=alert_status)](https://sonarcloud.io/dashboard?id=litetex_CoreFrameworkBase) <br>[![Latest workflow runs](https://img.shields.io/github/actions/workflow/status/litetex/CoreFramework/sonar.yml?branch=dev)](https://github.com/litetex/CoreFramework/actions/workflows/sonar.yml?query=branch%3Adev)  |
| Check Build | [![Latest workflow runs](https://img.shields.io/github/actions/workflow/status/litetex/CoreFramework/checkBuild.yml?branch=dev)](https://github.com/litetex/CoreFramework/actions/workflows/checkBuild.yml?branch%3Adev) |
| Build Nuget | [![Latest workflow runs](https://img.shields.io/github/actions/workflow/status/litetex/CoreFramework/buildNuget.yml?branch=dev)](https://github.com/litetex/CoreFramework/actions/workflows/buildNuget.yml?branch%3Adev) |
| Release | [![master workflow runs](https://img.shields.io/github/actions/workflow/status/litetex/CoreFramework/release.yml?branch=master&label=master)](https://github.com/litetex/CoreFramework/actions/workflows/release.yml?query=branch%3Amaster) <br>[![master workflow runs](https://img.shields.io/github/actions/workflow/status/litetex/CoreFramework/release.yml?branch=master-release-test&label=release-test)](https://github.com/litetex/CoreFramework/actions/workflows/release.yml?query=branch%3Amaster-release-test) |
