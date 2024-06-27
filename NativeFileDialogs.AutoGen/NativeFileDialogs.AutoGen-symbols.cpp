#include <nfd.h>
#include <new>

nfdpathsetenum_t& (nfdpathsetenum_t::*_0)(nfdpathsetenum_t&&) = &nfdpathsetenum_t::operator=;
nfdu8filteritem_t& (nfdu8filteritem_t::*_1)(nfdu8filteritem_t&&) = &nfdu8filteritem_t::operator=;
nfdnfilteritem_t& (nfdnfilteritem_t::*_2)(nfdnfilteritem_t&&) = &nfdnfilteritem_t::operator=;
nfdwindowhandle_t& (nfdwindowhandle_t::*_3)(nfdwindowhandle_t&&) = &nfdwindowhandle_t::operator=;
nfdopendialogu8args_t& (nfdopendialogu8args_t::*_4)(nfdopendialogu8args_t&&) = &nfdopendialogu8args_t::operator=;
nfdopendialognargs_t& (nfdopendialognargs_t::*_5)(nfdopendialognargs_t&&) = &nfdopendialognargs_t::operator=;
nfdsavedialogu8args_t& (nfdsavedialogu8args_t::*_6)(nfdsavedialogu8args_t&&) = &nfdsavedialogu8args_t::operator=;
nfdsavedialognargs_t& (nfdsavedialognargs_t::*_7)(nfdsavedialognargs_t&&) = &nfdsavedialognargs_t::operator=;
nfdpickfolderu8args_t& (nfdpickfolderu8args_t::*_8)(nfdpickfolderu8args_t&&) = &nfdpickfolderu8args_t::operator=;
nfdpickfoldernargs_t& (nfdpickfoldernargs_t::*_9)(nfdpickfoldernargs_t&&) = &nfdpickfoldernargs_t::operator=;
nfdresult_t (*_10)(wchar_t**, const nfdopendialognargs_t*) = &::NFD_OpenDialogN_With;
nfdresult_t (*_11)(char**, const nfdopendialogu8args_t*) = &::NFD_OpenDialogU8_With;
nfdresult_t (*_12)(const void**, const nfdopendialognargs_t*) = &::NFD_OpenDialogMultipleN_With;
nfdresult_t (*_13)(const void**, const nfdopendialogu8args_t*) = &::NFD_OpenDialogMultipleU8_With;
nfdresult_t (*_14)(wchar_t**, const nfdsavedialognargs_t*) = &::NFD_SaveDialogN_With;
nfdresult_t (*_15)(char**, const nfdsavedialogu8args_t*) = &::NFD_SaveDialogU8_With;
nfdresult_t (*_16)(wchar_t**, const nfdpickfoldernargs_t*) = &::NFD_PickFolderN_With;
nfdresult_t (*_17)(char**, const nfdpickfolderu8args_t*) = &::NFD_PickFolderU8_With;
nfdresult_t (*_18)(const void**, const nfdpickfoldernargs_t*) = &::NFD_PickFolderMultipleN_With;
nfdresult_t (*_19)(const void**, const nfdpickfolderu8args_t*) = &::NFD_PickFolderMultipleU8_With;
