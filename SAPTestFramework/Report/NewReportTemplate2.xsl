<?xml version="1.0" encoding="gb2312"?>
<!--ToDo: 1. 删除主页上的WorkFlow-->
<!--ToDo: 2. 移动Filter到侧边栏的WorkFlow-->
<!--ToDo: 3. 加System Step到主页-->
<!--ToDo: 4. 加Output Data到主页-->
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="2.0" xpath-default-namespace="http://www.w3.org/1999/xhtml">
  <xsl:output method="html" doctype-system="http://www.w3.org/TR/xhtml1/DTD/xhtml1- transitional.dtd" doctype-public="-//W3C//DTD XHTML 1.0 Transitional// EN" indent="yes"/>

  <!--Summary-->
  <xsl:template match="Summary">
    <p style="color:#07ade0;">
      <strong>
        <xsl:value-of select="TestName"/>
      </strong>
    </p>
    <hr width="100%" color="navy" size="1" align="left"/>
    <ul data-role="listview" data-inset="true" data-theme="d" data-divider-theme="b">
      <li data-role="list-divider">System Information</li>
      <li>
        <table data-role="table" id="SystemInformation" data-mode="reflow" class="ui-responsive table-stroke ui-table ui-table-reflow" data-column-btn-theme="d" style="font-size:12px;">
          <tbody>
            <tr>
              <th>Overall Status:</th>
              <xsl:choose>
                <xsl:when test="OverallStatus = 'Pass'">
                  <td style="color:#00cc66">
                    <xsl:value-of select="OverallStatus"/>
                  </td>
                </xsl:when>
                <xsl:otherwise>
                  <td style="color:red">
                    <xsl:value-of select="OverallStatus"/>
                  </td>
                </xsl:otherwise>
              </xsl:choose>
            </tr>
            <tr>
              <th>Company Code:</th>
              <td>
                <xsl:value-of select="CompanyCode"/>
              </td>
            </tr>
            <tr>
              <th>Asset: </th>
              <td>
                <xsl:value-of select="Asset"/>
              </td>
            </tr>
            <tr>
              <th>Box NameList: </th>
              <td>
                <xsl:value-of select="BoxNameList"/>
              </td>
            </tr>
            <tr>
              <th>Total Steps: </th>
              <td>
                <xsl:value-of select="TotalSteps"/>
              </td>
            </tr>
            <tr>
              <th>Runtime: </th>
              <td>
                <xsl:value-of select="Runtime"/>
              </td>
            </tr>
            <tr>
              <th>Run Mode: </th>
              <td>
                <xsl:value-of select="RunMode"/>
              </td>
            </tr>
            <tr>
              <th>Test Machine: </th>
              <td>
                <xsl:value-of select="TestMachine"/>
              </td>
            </tr>
            <tr>
              <th>SAP Version: </th>
              <td>
                <xsl:value-of select="SAPVersion"/>
              </td>
            </tr>
            <tr>
              <th>TimeZone: </th>
              <td>
                <xsl:value-of select="TimeZone"/>
              </td>
            </tr>
            <tr>
              <th>Start Time: </th>
              <td>
                <xsl:value-of select="TestStartTime"/>
              </td>
            </tr>
            <tr>
              <th>End Time: </th>
              <td>
                <xsl:value-of select="TestEndTime"/>
              </td>
            </tr>
            <tr>
              <th>Duration: </th>
              <td>
                <xsl:value-of select="Duration"/>
              </td>
            </tr>
            <tr>
              <th>Executor: </th>
              <td>
                <xsl:value-of select="Executor"/>
              </td>
            </tr>
          </tbody>
        </table>
      </li>
    </ul>
  </xsl:template>

  <!--Detail-->
  <xsl:template match="Detail">
    <xsl:for-each select="TestStep">
      <xsl:apply-templates select=".">
        <xsl:with-param name="index" select="position()"/>
      </xsl:apply-templates>
    </xsl:for-each>
  </xsl:template>

  <!--Test Case-->
  <xsl:template match="TestStep">
    <xsl:param name="index"/>
    <div data-role="page" id="Page_{$index}" class="mainPages" data-theme="b">
      <header data-role="header" data-position="fixed" data-theme="b">
        <h1>
          <xsl:value-of select="CaseName"/>
        </h1>
        <a href="#HomePage" data-target="main" data-icon="home">&#12288;</a>
      </header>
      <div data-role="content">
        <p style="color:#07ade0;">
          <strong>
            <xsl:value-of select="CaseName"/>
          </strong>
        </p>
        <hr width="100%" color="navy" size="1" align="left"/>
        <ul data-role="listview" data-inset="true" data-theme="d" data-divider-theme="b">
          <li data-role="list-divider">Overview</li>
          <li>
            <table data-role="table" id="movie-table" data-mode="reflow" class="ui-responsive table-stroke ui-table ui-table-reflow">
              <thead>
                <tr>
                  <th data-priority="3">BoxName</th>
                  <th data-priority="2">Check Points</th>
                  <th data-priority="2">Status</th>
                </tr>
              </thead>
              <tbody>
                <tr>
                  <td>
                    <xsl:value-of select="BoxName"/>
                  </td>
                  <td>
                    <xsl:value-of select="count(CheckPoints/CheckPoint)"/>
                  </td>
                  <xsl:choose>
                    <xsl:when test="CaseStatus = 'Pass'">
                      <td  class="status" style="color:#00cc66">
                        <xsl:value-of select="CaseStatus"/>
                      </td>
                    </xsl:when>
                    <xsl:when test="CaseStatus = 'Fail'">
                      <td class="status" style="color:red">
                        <xsl:value-of select="CaseStatus"/>
                      </td>
                    </xsl:when>
                    <xsl:when test="CaseStatus = 'Runtime Error'">
                      <td class="status">
                        <a href="{ErrorSnapshot}" style="color:red">
                          <xsl:value-of select="CaseStatus"/>
                        </a>
                      </td>
                    </xsl:when>
                  </xsl:choose>
                </tr>
              </tbody>
            </table>
          </li>
        </ul>

        <ul data-role="listview" data-inset="true" data-theme="d" data-divider-theme="b">
          <li data-role="list-divider">Input Data</li>
          <xsl:apply-templates select="InputDatas"/>
        </ul>
        <ul data-role="listview" data-inset="true" data-theme="d" data-divider-theme="b">
          <li data-role="list-divider">Output Data</li>
          <xsl:apply-templates select="OutputDatas"/>
        </ul>
        <xsl:apply-templates select="ItemList"/>
        <xsl:apply-templates select="CheckPoints"/>
        <xsl:apply-templates select="TestLog">
          <xsl:with-param name="Log_Index" select="$index"/>
        </xsl:apply-templates>
      </div>
    </div>
  </xsl:template>

  <xsl:template match="InputData|OutputData">
    <th>
      <xsl:value-of select="FieldName"/>
    </th>
    <td>
      <xsl:value-of select="FieldValue"/>
    </td>
  </xsl:template>

  <!-- Input Data -->
  <xsl:template match="InputDatas">
    <li>
      <table data-role="table" id="movie-table" data-mode="reflow" class="ui-responsive table-stroke ui-table ui-table-reflow" style="font-size:12px;">
        <thead>
          <tr>
            <th style="width:10%"></th>
            <th style="width:20%"></th>
            <th style="width:10%"></th>
            <th style="width:20%"></th>
          </tr>
        </thead>
        <tbody>
          <xsl:choose>
            <xsl:when test="count(InputData) &gt; 0 ">
              <xsl:for-each select="InputData">
                <xsl:if test="position() mod 2=1">
                  <tr>
                    <xsl:apply-templates select="."/>
                    <xsl:apply-templates select="following-sibling::InputData[position()=1]"/>
                  </tr>
                </xsl:if>
              </xsl:for-each>
            </xsl:when>
            <xsl:otherwise>
              <tr>
                <td class="parameter_Value">No input data</td>
              </tr>
            </xsl:otherwise>
          </xsl:choose>
        </tbody>
      </table>
      <p/>
    </li>
  </xsl:template>



  <!--Output Data-->
  <xsl:template match="OutputDatas">
    <!--     <ul data-role="listview" data-inset="true" data-theme="d" data-divider-theme="b">
      <li data-role="list-divider">Output Data</li> -->
    <li>
      <table data-role="table" id="movie-table" data-mode="reflow" class="ui-responsive table-stroke ui-table ui-table-reflow" style="font-size:12px;">
        <thead>
          <tr>
            <th style="width:10%"> </th>
            <th style="width:20%"> </th>
            <th style="width:10%"> </th>
            <th style="width:20%"> </th>
          </tr>
        </thead>
        <tbody>
          <xsl:choose>
            <xsl:when test="count(OutputData) &gt; 0">
              <xsl:for-each select="OutputData">
                <xsl:if test="position() mod 2=1">
                  <tr>
                    <xsl:apply-templates select="."/>
                    <xsl:apply-templates select="following-sibling::OutputData[position()=1]"/>
                  </tr>
                </xsl:if>
              </xsl:for-each>
            </xsl:when>
            <xsl:otherwise>
              <tr>
                <td class="parameter_Value">No Output data</td>
              </tr>
            </xsl:otherwise>
          </xsl:choose>
        </tbody>
      </table>
    </li>
    <!--     </ul> -->
  </xsl:template>

  <!--Item List -->
  <xsl:template match="ItemList">
    <ul data-role="listview" data-inset="true" data-theme="d" data-divider-theme="b">
      <li data-role="list-divider">Item List</li>
      <li>
        <div style="overflow:auto;width:100%;">
          <table data-role="table" id="movie-table" data-mode="reflow" class="ui-responsive table-stroke ui-table ui-table-reflow" style="font-size:12px;">
            <xsl:apply-templates/>
          </table>
        </div>
      </li>
    </ul>
  </xsl:template>
  <xsl:template match="ColumnNames">
    <thead>
      <tr>
        <xsl:for-each select="ColumnName">
          <th>
            <xsl:value-of select="."/>
          </th>
        </xsl:for-each>
      </tr>
    </thead>
  </xsl:template>
  <xsl:template match="ColumnValues">
    <tr>
      <xsl:for-each select="ColumnValue">
        <td>
          <xsl:value-of select="."/>
        </td>
      </xsl:for-each>
    </tr>
  </xsl:template>

  <!-- Checkpoints-->
  <xsl:template match="CheckPoints">
    <ul data-role="listview" data-inset="true" data-theme="d" data-divider-theme="b">
      <li data-role="list-divider">Checkpoint</li>
      <li>
        <table data-role="table" id="movie-table" data-mode="reflow" class="ui-responsive table-stroke ui-table ui-table-reflow" style="font-size:12px;">
          <thead>
            <tr>
              <th style="width:5%">No.</th>
              <th style="width:15%">StepName</th>
              <th style="width:15%">CheckpointName</th>
              <th style="width:8%">Time</th>
              <th style="width:8%">Status</th>
              <th style="width:10%">OriginalValue</th>
              <th style="width:20%">ExpectedResult</th>
              <th style="width:20%">ActualResult</th>
              <th style="width:20%">CompareMode</th>
            </tr>
          </thead>
          <tbody>
            <xsl:apply-templates/>
          </tbody>
        </table>
      </li>
    </ul>
  </xsl:template>
  <xsl:template match="CheckPoint">
    <tr>
      <td>
        <xsl:value-of select="CPSN"/>
      </td>
      <td>
        <xsl:value-of select="CPStepName"/>
      </td>
      <td>
        <xsl:value-of select="CPName"/>
      </td>
      <td>
        <xsl:value-of select="CPTime"/>
      </td>
      <td>
        <xsl:choose>
          <xsl:when test="CPStatus = 'Pass' and CPSnapshot !='' ">
            <a id="checkpoint" href="{CPSnapshot}" data-ajax="false" style="color:green">Pass</a>
          </xsl:when>
          <xsl:when test="CPStatus = 'Pass' and CPSnapshot ='' ">
            <a style="color:green">Pass</a>
          </xsl:when>
          <xsl:when test="CPStatus = 'Fail' and CPSnapshot !='' ">
            <a id="checkpoint" href="{CPSnapshot}" data-ajax="false" style="color:red">Fail</a>
          </xsl:when>
          <xsl:when test="CPStatus = 'Fail' and CPSnapshot ='' ">
            <a style="color:red">Fail</a>
          </xsl:when>
          <xsl:when test="CPStatus = 'Can not verify' and CPSnapshot !='' ">
            <a id="checkpoint" href="{CPSnapshot}" data-ajax="false" style="color:orange">Can not verify</a>
          </xsl:when>
          <xsl:when test="CPStatus = 'Can not verify' and CPSnapshot ='' ">
            <a style="color:orange">Can not verify</a>
          </xsl:when>
        </xsl:choose>
      </td>
      <td>
        <xsl:value-of select="CPOriginalValue"/>
      </td>
      <td>
        <xsl:value-of select="CPExpected"/>
      </td>
      <td>
        <xsl:value-of select="CPActual"/>
      </td>
      <td>
        <xsl:value-of select="CPCompareMode"/>
      </td>
    </tr>
  </xsl:template>

  <!-- TestLog-->
  <xsl:template match="TestLog">
    <xsl:param name="Log_Index"/>
    <ul data-role="listview" data-inset="true" data-theme="d" data-divider-theme="b">
      <li data-role="list-divider" data-icon="arrow-d" data-iconpos="left">
        <!--switcher-->
        <div onclick="toggle_child_visibility('ExecutionLog_{$Log_Index}')" style="font-size: 1em">
          Execution Log
        </div>
      </li>
      <li>
        <!-- hidden content-->
        <div id = "ExecutionLog_{$Log_Index}" style="display:none">
          <table data-role="table" id="movie-table" data-mode="reflow" class="ui-responsive table-stroke ui-table ui-table-reflow" style="font-size:12px;">
            <thead>
              <tr>
                <th style="width:10%">Current Time</th>
                <th style="width:10%">Status</th>
                <th style="width:15%">Event Name</th>
                <th style="width:65%">Event Description</th>
              </tr>
            </thead>
            <tbody>
              <xsl:apply-templates/>
            </tbody>
          </table>
        </div>
      </li>
    </ul>
  </xsl:template>
  <xsl:template match="Log">
    <tr>
      <td style="color:black">
        <xsl:value-of select="@CurrentTime"/>
      </td>
      <xsl:choose>
        <xsl:when test="@Status = 'Done'">
          <td style="color:black">
            <xsl:value-of select="@Status"/>
          </td>
        </xsl:when>
        <xsl:when test="@Status = 'Pass'">
          <td style="color:#00cc66">
            <xsl:value-of select="@Status"/>
          </td>
        </xsl:when>
        <xsl:when test="@Status = 'Warning'">
          <td style="color:orange">
            <xsl:value-of select="@Status"/>
          </td>
        </xsl:when>
        <xsl:when test="@Status = 'Fail'">
          <td style="color:red">
            <xsl:value-of select="@Status"/>
          </td>
        </xsl:when>
      </xsl:choose>
      <td style="color:black">
        <xsl:value-of select="@EventName"/>
      </td>
      <td style="color:black">
        <xsl:value-of select="."/>
      </td>
    </tr>
  </xsl:template>

  <xsl:template match="/">
    <html class="no-js multiview">
      <head>
        <title>FAIT Execution Report</title>
        <meta charset="utf-8"/>
        <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no"/>
        <!-- css -->
        <link rel="stylesheet" type="text/css" href="../_config/css/jquery.mobile-1.0.min.css"/>
        <!--<link rel="stylesheet" type="text/css" href="css/jquery.mobile-1.3.1.min.css" /> -->
        <!--<link rel="stylesheet" type="text/css" href="css/jquery.mobile.scrollview.css" /> -->
        <link rel="stylesheet" type="text/css" href="../_config/css/jquery.mobile.multiview.css"/>
        <link rel="stylesheet" type="text/css" href="../_config/css/jquery.fancybox-1.3.4.css"/>
        <!-- javascript -->
        <script type="text/javascript" src="../_config/js/jquery-1.6.4.min.js"/>
        <script type="text/javascript" src="../_config/js/jquery.mobile-1.0multiview.js"/>
        <!--<script type="text/javascript" src="js/jquery.easing.1.3.js"></script> -->
        <!--<script type="text/javascript" src="js/jquery.mobile.scrollview.js"></script>-->
        <script type="text/javascript" src="../_config/js/jquery.mobile.multiview.js"/>
        <script type="text/javascript" src="../_config/js/jquery.fancybox-1.3.4.pack.js"/>
      </head>
      <body>
        <script type="text/javascript">
          $(document).ready(function() {
          /* This is basic - uses default settings */
          $("a#checkpoint").fancybox({
          'showCloseButton' :	true,
          'transitionIn' : 'elastic',
          'transitionOut' : 'fade',
          'zoomSpeedIn': 500,
          'zoomSpeedOut': 300,
          'titleShow' : true,
          'hideOnContentClick': true
          });
          });

          function toggle_child_visibility(element_id) {
          if (document.getElementById(element_id) != null) {
          $("#"+ element_id).slideToggle();
          }
          }
        </script>

        <div data-role="page" id="wrap1" data-wrapper="true" data-show="first">
          <!-- ////////////// embedded menu panel /////////////  -->
          <div data-role="panel" data-id="menu" data-hash="history" data-panel="menu">
            <div data-role="page" id="menuPage" data-show="first" data-theme="b">
              <div data-role="header" data-theme="b">
              </div>
              <div data-role="content">
                <ol data-role="listview" data-inset="true" data-theme="d" data-divider-theme="b"  data-filter="true">
                  <li data-role="list-divider">Workflow</li>
                  <xsl:for-each select="ReportRoot/Detail/TestStep">
                    <li>
                      <a href="#Page_{position()}" data-target="main" data-transition="fade">
                        <xsl:value-of select="CaseName"/>
                        <xsl:choose>
                          <xsl:when test="CaseStatus = 'Pass'">
                            <span class="ui-li-count" style="color:#00cc66">&#x2713;</span>
                          </xsl:when>
                          <xsl:otherwise>
                            <span class="ui-li-count" style="color:red">&#x2717;</span>
                          </xsl:otherwise>
                        </xsl:choose>
                      </a>
                    </li>
                  </xsl:for-each>
                </ol>
              </div>
            </div>
          </div>
          <!-- MenuPage page-->
          <!-- ////////////// embedded main panel ///////////////  -->
          <div data-role="panel" data-id="main" data-panel="main" data-hash="history">
            <div data-role="page" id="HomePage" data-show="first" class="mainPages" data-theme="b">
              <Header data-role="header" data-theme="b">
                <h1>FAIT Execution Report</h1>
              </Header>

              <div data-role="content">
                <xsl:apply-templates select="ReportRoot/Summary"/>

                <ul data-role="listview" data-inset="true" data-theme="d" data-divider-theme="b">
                  <li data-role="list-divider">Input Data</li>
                  <xsl:for-each select="ReportRoot/Detail/TestStep">
                    <li>
                      <xsl:value-of select="position()"/>. <xsl:value-of select="CaseName"/>
                    </li>
                    <xsl:apply-templates select="InputDatas"/>
                  </xsl:for-each>
                </ul>

              </div>

              <div data-role="content">


                <ul data-role="listview" data-inset="true" data-theme="d" data-divider-theme="b">
                  <li data-role="list-divider">Output Data</li>
                  <xsl:for-each select="ReportRoot/Detail/TestStep">
                    <li>
                      <xsl:value-of select="position()"/>. <xsl:value-of select="CaseName"/>
                    </li>
                    <xsl:apply-templates select="OutputDatas"/>
                  </xsl:for-each>
                </ul>

              </div>


            </div>

            <xsl:apply-templates select="ReportRoot/Detail"/>
          </div>
          <!-- end panel -->
          <!-- global footer across all panel pages -->
          <div data-role="footer" data-theme="b" data-position="fixed">
            <p class="jqm-version"/>
            <p>@FAIT Automation Team</p>
          </div>
        </div>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>
