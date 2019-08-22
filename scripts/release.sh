#!/bin/bash

RELEASE_VERSION=$1
NEXT_VERSION=$2
MASTER_BRANCH=master

if [[ -z ${RELEASE_VERSION} ]]; then
    echo Release version cannot be empty!
    exit 1
fi

if [[ -z ${NEXT_VERSION} ]]; then
    echo Next version cannot be empty!
    exit 1
fi

echo "Releasing version '${RELEASE_VERSION}'"
echo "Next version will be '${NEXT_VERSION}'"
echo "Master branch: '${MASTER_BRANCH}'"

git checkout ${MASTER_BRANCH} || { echo "Failed to checkout to branch ${MASTER_BRANCH}. Aborting..."; exit 1; }

find . -type f -name *.csproj | xargs sed -i -e "s/<VersionPrefix>.*<\/VersionPrefix>/<VersionPrefix>${RELEASE_VERSION}<\/VersionPrefix>/"
find . -type f -name *.csproj | xargs sed -i -e "s/<VersionSuffix>.*<\/VersionSuffix>/<VersionSuffix><\/VersionSuffix>/"
find . -name *.csproj -o -name *.nuspec | xargs cat | grep "SNAP" && { echo "Snapshot versions of nuget packages found. Aborting..."; exit 1; }

git add *.csproj || { echo "Failed to add .csproj files for release commit! Aborting..."; exit 1; }
git commit -m "Released version ${RELEASE_VERSION}" || { echo "Failed to commit release! Aborting..."; exit 1; }

find . -type f -name *.csproj | xargs sed -i -e "s/<VersionPrefix>${RELEASE_VERSION}<\/VersionPrefix>/<VersionPrefix>${NEXT_VERSION}<\/VersionPrefix>/"
find . -type f -name *.csproj | xargs sed -i -e "s/<VersionSuffix><\/VersionSuffix>/<VersionSuffix>SNAPSHOT<\/VersionSuffix>/"

git add *.csproj || { echo "Failed to add .csproj files for changed version commit! Aborting..."; exit 1; }
git commit -m "Changed version to ${NEXT_VERSION}-SNAPSHOT" || { echo "Failed to commit changed version! Aborting..."; exit 1; }

git push origin HEAD:refs/for/${MASTER_BRANCH} ||  { echo "Failed to push release to branch ${MASTER_BRANCH}!! Aborting..."; exit 1; }

echo "Release commits successfully created and pushed!"
echo "Released version '${RELEASE_VERSION}'"
echo "Next version will be '${NEXT_VERSION}'"
