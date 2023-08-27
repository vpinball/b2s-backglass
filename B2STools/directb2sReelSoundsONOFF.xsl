<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:output method="xml" omit-xml-declaration="yes" />
	<!-- directb2sReelSoundsONOFF.xsl xslt transformation to add or remove empty attributes for directb2s files -->

	<!-- Identity template: copy all nodes and attributes as-is -->
	<xsl:template match="@* | node()">
		<xsl:copy>
			<xsl:apply-templates select="@* | node()"/>
		</xsl:copy>
	</xsl:template>
	
	<!-- Match the "Score" node and add the "Sound3-5" empty attribute -->
	<xsl:template match="Score[not(@Sound3)]">
		<xsl:message>
Adding the Sound3-5 attributes
		</xsl:message>

		<xsl:copy>
			<xsl:apply-templates select="@*"/>
			<xsl:attribute name="Sound3"></xsl:attribute>
			<xsl:attribute name="Sound4"></xsl:attribute>
			<xsl:attribute name="Sound5"></xsl:attribute>
			<xsl:apply-templates select="node()"/>
		</xsl:copy>
	</xsl:template>

	<!-- Match the "Score" with a "Sound3-5" node and remove them -->
	<xsl:template match="Score[(@Sound3)]">
		<xsl:message>
Removing the Sound3-5 attributes
		</xsl:message>
		<xsl:if test="@Sound3!=''">
			<xsl:message terminate="yes">
The sound attribute isn't empty! exiting
			</xsl:message>
		</xsl:if>
	
		<xsl:copy>
			<xsl:apply-templates select="@* | node()" mode="RemoveSound" />
		</xsl:copy>
	</xsl:template>

	<xsl:template mode="RemoveSound" match="@Sound3 | @Sound4 | @Sound5" />
	<xsl:template mode="RemoveSound" match="@* | node()">
		<xsl:copy>
			<xsl:apply-templates select="@* | node()"/>
		</xsl:copy>
	</xsl:template>
</xsl:stylesheet>
