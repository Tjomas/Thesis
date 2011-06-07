#!/bin/bash

rm Unity/InteractionApp/Assets/dlls/IPlugin.dll
rm Unity/InteractionApp/Assets/resources/plugins/InteractionOne.bytes

cp Unity/IPlugin/InteractionOne/bin/Debug/IPlugin.dll Unity/InteractionApp/Assets/dlls/
cp Unity/IPlugin/InteractionOne/bin/Debug/InteractionOne.dll Unity/InteractionApp/Assets/resources/plugins/
mv Unity/InteractionApp/Assets/resources/plugins/InteractionOne.dll Unity/InteractionApp/Assets/resources/plugins/InteractionOne.bytes