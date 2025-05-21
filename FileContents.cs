namespace SampleWinformsPlugin
{
    public class FileContents
    {
        public static string JAVA_MAIN_CLASS_CONTENT = "public class Main {\n  public static void main(String[] args) {\n        System.out.println(\"Hello World!\");\n   }\n}";
        public static string POM_CONTENT = "<project xmlns=\"http://maven.apache.org/POM/4.0.0\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xsi:schemaLocation=\"http://maven.apache.org/POM/4.0.0 http://maven.apache.org/xsd/maven-4.0.0.xsd\">\n    <modelVersion>4.0.0</modelVersion>\n    <groupId>REPLACE_GROUP_ID</groupId>\n   <artifactId>REPLACE_ARTIFACT_ID</artifactId>\n  <version>REPLACE_VERSION</version>\n    <dependencies>\n    </dependencies>\n   <build>\n       <plugins>\n     </plugins>\n    </build>\n</project>";
    }
}
