Android setup -

Player Settings -> Publishing -> [Insert Picture here]

Android manifest inside the <manifest> tags:

    <uses-permission android:name="com.arctop.permission.ARCTOP_DATA" />

baseProjectTemplate.grade:

Add

    id "org.jetbrains.kotlin.jvm" version "1.9.20"

Before **BUILD_SCRIPT_DEPS**

Verify the correct version for kotlin and your project

Delete

    task clean(type: Delete) {
    delete rootProject.buildDir
    }

gradleTemplate.properties:

Add

    android.useAndroidX=true

Before unityStreamingAssets=**STREAMING_ASSETS**
