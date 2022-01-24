<?xml version="1.0" encoding="utf-8"?>

<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

	<xsl:output method="xml" indent="yes" encoding="utf-8" omit-xml-declaration="yes"/>


	<!-- Replace root node name Menues with MenuItems
       and call MenuListing for its children-->
	<xsl:template match="/GRUPOS">
		<MenuItems>
			<xsl:call-template name="MenuListing"></xsl:call-template>
		</MenuItems>

		
		
	</xsl:template>

	<!-- Allow for recursive child nodeprocessing -->
	<xsl:template name="MenuListing">
				<xsl:apply-templates select="Grupo" />
	</xsl:template>

	
	<xsl:template match="Grupo">
			<MenuItem>
				<!-- Convert Menu child elements to MenuItem attributes<xsl:text><![CDATA[ ]]></xsl:text> count(Grupo) -->
				<xsl:attribute name="gru_nome">
					<xsl:value-of select='gru_nome'/>
					<!--<xsl:if test="count(Grupo) = 0">
					  <xsl:text>(</xsl:text>
				      <xsl:value-of select='gru_quantidadeProduto'/>
					  <xsl:text>)</xsl:text>
					</xsl:if>
					<xsl:if test="gru_bloquear = 'true'">
					  <xsl:text><![CDATA[(<img src="imagens/bloqueadoVenda.png" style="height: 8px;" />)]]></xsl:text>
				      
						<xsl:value-of select='gru_bloquear'/>
					  <xsl:text>)</xsl:text>
					  </xsl:if>-->
				  </xsl:attribute>

				<xsl:attribute name="gru_id">
					<xsl:value-of select="gru_id"/>
				</xsl:attribute>

				<xsl:attribute name="gru_imagem">
					<xsl:value-of select="gru_imagem"/>
					<xsl:if test="gru_bloquear = 'true'">
					  <xsl:text>imagens/GrupoBloqueado.png</xsl:text>
					</xsl:if>

					<xsl:if test="gru_subBloquear = 'true'">
						<xsl:text>imagens/GrupoSubBloqueado.png</xsl:text>
					</xsl:if>
					

				</xsl:attribute>


				<!-- Call MenuListing if there are child Menu nodes -->
				<xsl:if test="count(Grupo) > 0">
					<xsl:call-template name="MenuListing" />
				</xsl:if>

			</MenuItem>



			<!--<MenuItem> 
			 Convert Menu child elements to MenuItem attributes 
			 <xsl:attribute name="GRU_NOME">

				<xsl:value-of select='GRU_NOME'/>

				<xsl:if test="count(PRO) >0">
					<xsl:value-of select='GRU_NOME'/>
					<xsl:value-of select='count(PRO)'/>
				</xsl:if>
				<xsl:if test="count(PRO) = 0">
					<xsl:value-of select='GRU_NOME'/>
				</xsl:if>

	
				<xsl:if test="GRU_NOME != 'GRUPOS'">
						<xsl:value-of select='GRU_NOME'/>
				</xsl:if>

			</xsl:attribute>
			<xsl:attribute name="ToolTip">
				<xsl:value-of select="GRU_NOME"/>
			</xsl:attribute>
	
			<xsl:attribute name="Value">
				<xsl:value-of select="GRU_ID"/>
			</xsl:attribute>
			
			<xsl:attribute name="NavigateUrl">
				<xsl:text>?gru_id=</xsl:text>
				<xsl:value-of select="GRU_ID"/>
			</xsl:attribute>
			<xsl:attribute name="Target">
				<xsl:value-of select="_blank"/>
			</xsl:attribute>

			<xsl:if test="count(GRU) >0">

				<xsl:call-template name="MenuListing" />
			</xsl:if>
			</MenuItem>-->
	</xsl:template>



</xsl:stylesheet>