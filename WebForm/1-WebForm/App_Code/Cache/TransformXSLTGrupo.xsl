<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="xml" indent="yes" encoding="utf-8"/>
  <!-- Find the root node called Menus
and call MenuListing for its children -->
  <xsl:template match="/GRUPOS">
    <MenuItems>
      <xsl:call-template name="MenuListing" />
    </MenuItems>
  </xsl:template>
  <!-- Allow for recusive child node processing -->
  <xsl:template name="MenuListing">
    <xsl:apply-templates select="Grupo" />
  </xsl:template>

  <xsl:template match="Grupo">
    <MenuItem>
      <!-- Convert Menu child elements to MenuItem attributes -->
      <xsl:attribute name="gru_nome">
        <xsl:if test="gru_nome != 'GRUPOS'">
          <xsl:value-of select='gru_nome'/>         
        </xsl:if>
      </xsl:attribute>

      <xsl:attribute name="gru_nomeAmigavel">
        <xsl:value-of select="gru_nomeAmigavel"/>
      </xsl:attribute>

      <xsl:attribute name="gru_id">
        <xsl:value-of select="gru_id"/>
      </xsl:attribute>
      <!-- Call MenuListing if there are child Menu nodes -->
      <xsl:if test="count(Grupo) > 0">
        <xsl:call-template name="MenuListing" />
      </xsl:if>


    </MenuItem>



  </xsl:template>
</xsl:stylesheet>