#ifndef _ECMATERIALDROPTEXT_H
#define _ECMATERIALDROPTEXT_H

#include "ECOleMemDropTarget.h"

// ************************************************************
//	ECMaterialDropText
//	
//		Our implementation
// ************************************************************
class ECMaterialDropText : public ECOleMemDropTarget
{
public:
	ECMaterialDropText();

	//	called by child we have drop
	void	GotDrop(void);
	DWORD	GotDrag(void);
	void	GotLeave(void);
	DWORD	GotEnter(void);
};


#endif