#include <nfd.h>
#include <new>

nfdpathsetenum_t& (nfdpathsetenum_t::*_0)(nfdpathsetenum_t&&) = &nfdpathsetenum_t::operator=;
nfdu8filteritem_t& (nfdu8filteritem_t::*_1)(nfdu8filteritem_t&&) = &nfdu8filteritem_t::operator=;
nfdwindowhandle_t& (nfdwindowhandle_t::*_2)(nfdwindowhandle_t&&) = &nfdwindowhandle_t::operator=;
nfdopendialogu8args_t& (nfdopendialogu8args_t::*_3)(nfdopendialogu8args_t&&) = &nfdopendialogu8args_t::operator=;
nfdsavedialogu8args_t& (nfdsavedialogu8args_t::*_4)(nfdsavedialogu8args_t&&) = &nfdsavedialogu8args_t::operator=;
nfdpickfolderu8args_t& (nfdpickfolderu8args_t::*_5)(nfdpickfolderu8args_t&&) = &nfdpickfolderu8args_t::operator=;
nfdresult_t (*_6)(char**, const nfdopendialogu8args_t*) = &::NFD_OpenDialogN_With;
nfdresult_t (*_7)(char**, const nfdopendialogu8args_t*) = &::NFD_OpenDialogU8_With;
nfdresult_t (*_8)(const void**, const nfdopendialogu8args_t*) = &::NFD_OpenDialogMultipleN_With;
nfdresult_t (*_9)(const void**, const nfdopendialogu8args_t*) = &::NFD_OpenDialogMultipleU8_With;
nfdresult_t (*_10)(char**, const nfdsavedialogu8args_t*) = &::NFD_SaveDialogN_With;
nfdresult_t (*_11)(char**, const nfdsavedialogu8args_t*) = &::NFD_SaveDialogU8_With;
nfdresult_t (*_12)(char**, const nfdpickfolderu8args_t*) = &::NFD_PickFolderN_With;
nfdresult_t (*_13)(char**, const nfdpickfolderu8args_t*) = &::NFD_PickFolderU8_With;
nfdresult_t (*_14)(const void**, const nfdpickfolderu8args_t*) = &::NFD_PickFolderMultipleN_With;
nfdresult_t (*_15)(const void**, const nfdpickfolderu8args_t*) = &::NFD_PickFolderMultipleU8_With;
