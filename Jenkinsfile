pipeline {
    agent {
        label 'windows && dotnet && jenkins01'
    }
    options {
        timestamps()
    }
    environment{
        RUNTIME = 'dotnet'
    }
    stages {
        stage ('push'){
            when {
                environment name: 'GERRIT_EVENT_TYPE', value: 'patchset-created'
            }
            stages {
                stage('clean') {
                    steps {
                        bat 'git clean -dfx'
                        checkout scm
                        bat 'git clean -dfx'
                    }
                }
                stage('build') {
                    steps {
                        bat '%RUNTIME% build LinearLight2.sln -c Release'
                    }
                }
                stage ('test') {
                    steps {
                        bat '%RUNTIME% test LinearLight2Test/LinearLight2Test.csproj -c Release --logger:trx /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura'
                        step([$class: 'MSTestPublisher', testResultsFile:"LinearLight2Test/TestResults/*.trx", failOnError: true, keepLongStdio: true])
                        cobertura autoUpdateHealth: false, autoUpdateStability: false, coberturaReportFile: 'LinearLight2Test\\coverage.cobertura.xml', failUnhealthy: false, failUnstable: false, maxNumberOfBuilds: 0, onlyStable: false, sourceEncoding: 'ASCII', zoomCoverageChart: false, methodCoverageTargets: '80.0, 0.0, 0.0', lineCoverageTargets: '80.0, 0.0, 0.0', conditionalCoverageTargets: '70.0, 0.0, 0.0'
                    }
                }
                stage('pack') {
                    steps {
                        bat '%RUNTIME% pack LinearLight2/LinearLight2.csproj --configuration Release --include-source --version-suffix SNAPSHOT-%GERRIT_CHANGE_NUMBER%-%GERRIT_PATCHSET_NUMBER%'
                        bat '%RUNTIME% nuget push LinearLight2\\bin\\Release\\LinearLight2*SNAPSHOT*symbols.nupkg --source NetSnapshots'
                        bat '%RUNTIME% pack LinearLight2.NModbus/LinearLight2.NModbus.csproj --configuration Release --include-source --version-suffix SNAPSHOT-%GERRIT_CHANGE_NUMBER%-%GERRIT_PATCHSET_NUMBER%'
                        bat '%RUNTIME% nuget push LinearLight2.NModbus\\bin\\Release\\LinearLight2.NModbus*SNAPSHOT*symbols.nupkg --source NetSnapshots'
                    }
                }
                stage('analyse') {
                    steps {
                        bat 'dotnet tool restore'
                        bat 'dotnet jb inspectcode -o="code_inspection.xml" LinearLight2.sln'
                        recordIssues failOnError: true, qualityGates: [[threshold: 1, type: 'TOTAL_NORMAL', unstable: true]], tools: [resharperInspectCode(pattern: 'code_inspection.xml')]
                    }
                }
            }
        }
        stage ('merge') {
            when {
                environment name: 'GERRIT_EVENT_TYPE', value: 'change-merged'
            }
            stages {
                stage ('release') {
                    when {
                        expression {
                            return env.GERRIT_CHANGE_SUBJECT =~ '^Released version \\d*\\.\\d*\\.\\d*$'
                        }
                    }
                    environment {
                       RUNTIME = 'dotnet'
                    }
                    stages {
                        stage('clean') {
                            steps {
                                checkout scm
                                bat 'git clean -dfx'
                            }
                        }
                        stage('build') {
                            steps {
                                bat 'dotnet build LinearLight2/LinearLight2.csproj -c Release'
                                bat 'dotnet build LinearLight2.NModbus/LinearLight2.NModbus.csproj -c Release'
                            }
                        }
                        stage('pack') {
                            steps {
                                bat '%RUNTIME% pack LinearLight2/LinearLight2.csproj --configuration Release'
                                bat '%RUNTIME% nuget push LinearLight2\\bin\\Release\\LinearLight2*.nupkg --source nuget.org'
                                bat '%RUNTIME% pack LinearLight2.NModbus/LinearLight2.NModbus.csproj --configuration Release'
                                bat '%RUNTIME% nuget push LinearLight2.NModbus\\bin\\Release\\LinearLight2.NModbus*.nupkg --source nuget.org'
                            }
                        }
                    }
                }
            }
        }
    }
}
